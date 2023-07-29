using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class BonusDefinition : IBonusDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private AssetReferenceGameObject _bonusPrefab;
        [SerializeField] private EffectDefinitionAbstract _effect;

        public string Id => _id;
        public AssetReferenceGameObject BonusPrefab => _bonusPrefab;
        public EffectDefinitionAbstract EffectDefinition => _effect;
    }
}