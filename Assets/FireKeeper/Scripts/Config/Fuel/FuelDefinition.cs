using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class FuelDefinition : IFuelDefinition
    {
        [SerializeField] public string _id;
        [SerializeField] public float _power;
        [SerializeField] public float _slowdown;
        [SerializeField] public AssetReferenceGameObject _fuelPrefab;

        public string Id => _id;
        public float Power => _power;
        public float Slowdown => _slowdown;
        public AssetReferenceGameObject FuelPrefab => _fuelPrefab;
    }
}