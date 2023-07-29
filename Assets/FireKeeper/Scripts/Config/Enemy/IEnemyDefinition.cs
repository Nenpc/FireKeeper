using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    public interface IEnemyDefinition
    {
        public string Id { get; }
        public float ChaseRange { get; }
        public float AttackRange { get; }
        public AssetReferenceGameObject EnemyPrefab  { get; }
        public EffectDefinitionAbstract EffectDefinition { get; }
        public MovementDefinitionAbstract MovementDefinition { get; }
    }
}