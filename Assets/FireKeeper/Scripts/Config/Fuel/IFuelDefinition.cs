using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IFuelDefinition
    {
        string Id { get; }
        float Power { get; }
        float Slowdown { get; }
        AssetReferenceGameObject FuelPrefab { get; }
    }
}