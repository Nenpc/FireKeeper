using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class FuelFactory : IFuelFactory
    {
        public event Action<FuelView> OnCreate;
        public event Action<FuelView> OnDestroy;
        
        private readonly IFuelConfig _fuelConfig;
        private readonly Transform _parent;
        
        private readonly Dictionary<string, AddressablePool<FuelView>> _viewPools =
            new Dictionary<string, AddressablePool<FuelView>>();

        public FuelFactory(IFuelConfig fuelConfig, Transform paren)
        {
            _fuelConfig = fuelConfig;
            _parent = paren;
        }

        public void Dispose()
        {
            OnCreate = null;
            OnDestroy = null;
        }

        public async UniTask<FuelView> CreateRandomFuelAsync(Vector3 position)
        {
            var definition = _fuelConfig.GetRandomDefinition();
            return await CreateFuelAsync(definition, position);
        }

        public async UniTask<FuelView> CreateFuelBuIDAsync(string id, Vector3 position)
        {
            if (!_fuelConfig.HasDefinition(id))
            {
                Debug.LogError($"Can't create bonus, with id {id}!");
                return default;
            }

            var fuelDefinition = _fuelConfig.GetDefinition(id);
            return await CreateFuelAsync(fuelDefinition, position);
        }
        
        private async UniTask<FuelView> CreateFuelAsync(IFuelDefinition fuelDefinition, Vector3 position)
        {
            var pool = GetPool(fuelDefinition.Id);
            var fuelView = await pool.Get(fuelDefinition.FuelPrefab, position);

            if (!fuelView.TryGetComponent<InteractionMono>(out var interactionMono))
                interactionMono = fuelView.gameObject.AddComponent<InteractionMono>();

            interactionMono.Initialize(new InteractionInfo(fuelDefinition, typeof(TakeFuelState), () => Destroy(fuelView)));

            fuelView.Initialize(fuelDefinition);
            
            OnCreate?.Invoke(fuelView);
            
            return fuelView;
        }
        
        public void Destroy(FuelView fuelView)
        {
            OnDestroy?.Invoke(fuelView);

            var pool = GetPool(fuelView.FuelDefinition.Id);
            pool.Return(fuelView);
        }
        
        private AddressablePool<FuelView> GetPool(string addressableKey)
        {
            if (!_viewPools.TryGetValue(addressableKey, out var pool))
            {
                pool = new AddressablePool<FuelView>(addressableKey, _parent);
                _viewPools.Add(addressableKey, pool);
            }

            return pool;
        }
    }
}