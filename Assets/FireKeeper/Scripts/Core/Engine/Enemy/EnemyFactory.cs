using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        public event Action<EnemyView> OnCreate;
        public event Action<EnemyView> OnDestroy;
        
        private readonly IEnemyConfig _enemyConfig;
        private readonly IPlayerController _playerController;
        private readonly ICoreTimeController _coreTimeController;
        private readonly Transform _parent;
        
        private readonly Dictionary<string, AddressablePool<EnemyView>> _viewPools =
            new Dictionary<string, AddressablePool<EnemyView>>();

        public EnemyFactory(IEnemyConfig enemyConfig, 
            IPlayerController playerController, 
            ICoreTimeController coreTimeController,
            Transform parent)
        {
            _enemyConfig = enemyConfig;
            _playerController = playerController;
            _coreTimeController = coreTimeController;
            _parent = parent;
        }

        public void Dispose()
        {
            OnCreate = null;
            OnDestroy = null;
        }

        public async UniTask<EnemyView> CreateRandomEnemyAsync(Vector3 position)
        {
            var definition = _enemyConfig.GetRandomDefinition();
            return await CreateEnemyAsync(definition, position);
        }

        public async UniTask<EnemyView> CreateEnemyBuIDAsync(string id, Vector3 position)
        {
            if (!_enemyConfig.HasDefinition(id))
            {
                Debug.LogError($"Can't create enemy, with id {id}!");
                return default;
            }

            var enemyDefinition = _enemyConfig.GetDefinition(id);
            return await CreateEnemyAsync(enemyDefinition, position);
        }
        
        private async UniTask<EnemyView> CreateEnemyAsync(IEnemyDefinition enemyDefinition, Vector3 position)
        {
            var pool = GetPool(enemyDefinition.Id);
            var enemyView = await pool.Get(enemyDefinition.EnemyPrefab, position);
            var enemyController = new EnemyController(
                enemyDefinition, 
                this, 
                _playerController, 
                _coreTimeController, 
                enemyView);
            
            enemyView.Initialize(enemyController);
            
            OnCreate?.Invoke(enemyView);
            
            return enemyView;
        }
        
        public void DestroyEnemy(EnemyController enemyController)
        {
            var view = enemyController.GetView();
            OnDestroy?.Invoke(view);

            var pool = GetPool(enemyController.Definition.Id);
            pool.Return(view);
            view.RemoveController();
        }
        
        private AddressablePool<EnemyView> GetPool(string addressableKey)
        {
            if (!_viewPools.TryGetValue(addressableKey, out var pool))
            {
                pool = new AddressablePool<EnemyView>(addressableKey, _parent);
                _viewPools.Add(addressableKey, pool);
            }

            return pool;
        }
    }
}