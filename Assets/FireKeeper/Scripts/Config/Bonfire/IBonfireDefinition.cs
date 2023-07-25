using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IBonfireDefinition
    {
        int Life { get; }
        AssetReferenceGameObject BonfirePrefab { get; }
    }
}