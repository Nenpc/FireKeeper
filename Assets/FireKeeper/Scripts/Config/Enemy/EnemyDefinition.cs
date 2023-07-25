using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class EnemyDefinition : IEnemyDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private float _speed;
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _attackRange;
        [SerializeField] private AssetReferenceGameObject _enemyPrefab;
        [SerializeField] private EffectDefinitionAbstract _effectAbstract;

        public string Id => _id;
        public float Speed => _speed;
        public float ChaseRange => _chaseRange;
        public float AttackRange => _attackRange;
        public AssetReferenceGameObject EnemyPrefab => _enemyPrefab;
        public EffectDefinitionAbstract EffectAbstract => _effectAbstract;
    }
}