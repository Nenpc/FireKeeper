using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IBonusDefinition
    {
        string Id { get; }
        AssetReferenceGameObject BonusPrefab { get; }
        public EffectDefinitionAbstract EffectDefinition { get; }
    }
}