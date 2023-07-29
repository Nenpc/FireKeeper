using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IBonfireDefinition
    {
        int MaxLife { get; }
        float FadingPerSecond { get; }
        AssetReferenceGameObject BonfirePrefab { get; }
    }
}