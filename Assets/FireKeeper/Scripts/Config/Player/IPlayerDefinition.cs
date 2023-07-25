using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IPlayerDefinition
    {
        float StepSpeed { get; }
        float RunSpeed { get; }
        float Stamina { get; }
        AssetReferenceGameObject PlayerPrefab { get; }
    }
}