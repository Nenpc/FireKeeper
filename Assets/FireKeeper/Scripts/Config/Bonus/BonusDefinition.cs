using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class BonusDefinition : IBonusDefinition
    {
        [SerializeField] public string _id;
        [SerializeField] public int _time;
        [SerializeField] public AssetReferenceGameObject _bonusPrefab;
        [SerializeField] private EffectDefinitionAbstract _effectAbstract;

        public string Id => _id;
        public int Time => _time;
        public AssetReferenceGameObject BonusPrefab => _bonusPrefab;
        public EffectDefinitionAbstract EffectAbstract => _effectAbstract;
    }
}