using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IBonusDefinition
    {
        string Id { get; }
        int Time { get; }
        AssetReferenceGameObject BonusPrefab { get; }
        public EffectDefinitionAbstract EffectAbstract { get; }
    }
}