using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonusFactory : IBonusFactory, IDisposable
    {
        public event Action<BonusView> OnCreate;
        public event Action<BonusView> OnDestroy;
        
        private readonly IBonusConfig _bonusConfig;
        private readonly Transform _parent;
        
        private readonly Dictionary<string, AddressablePool<BonusView>> _viewPools =
            new Dictionary<string, AddressablePool<BonusView>>();

        public BonusFactory(IBonusConfig bonusConfig, Transform parent)
        {
            _bonusConfig = bonusConfig;
            _parent = parent;
        }

        public void Dispose()
        {
            OnCreate = null;
            OnDestroy = null;
        }

        public async UniTask<BonusView> CreateRandomBonusAsync(Vector3 position)
        {
            var definition = _bonusConfig.GetRandomDefinition();
            return await CreateBonusAsync(definition, position);
        }

        public async UniTask<BonusView> CreateBonusBuIDAsync(string id, Vector3 position)
        {
            if (!_bonusConfig.HasDefinition(id))
            {
                Debug.LogError($"Can't create bonus, with id {id}!");
                return default;
            }

            var bonusDefinition = _bonusConfig.GetDefinition(id);
            return await CreateBonusAsync(bonusDefinition, position);
        }
        
        private async UniTask<BonusView> CreateBonusAsync(IBonusDefinition bonusDefinition, Vector3 position)
        {
            var pool = GetPool(bonusDefinition.Id);
            var bonusView = await pool.Get(bonusDefinition.BonusPrefab, position);
            
            var bonusController = new BonusController(
                bonusDefinition, 
                this,
                bonusView);
            
            bonusView.Initialize(bonusController);

            OnCreate?.Invoke(bonusView);
            
            return bonusView;
        }
        
        public void Destroy(BonusView bonusView)
        {
            OnDestroy?.Invoke(bonusView);
            
            var pool = GetPool(bonusView.BonusController.Definition.Id);
            pool.Return(bonusView);
        }
        
        private AddressablePool<BonusView> GetPool(string addressableKey)
        {
            if (!_viewPools.TryGetValue(addressableKey, out var pool))
            {
                pool = new AddressablePool<BonusView>(addressableKey, _parent);
                _viewPools.Add(addressableKey, pool);
            }

            return pool;
        }
    }
}