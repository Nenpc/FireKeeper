using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public ParticleSystem ParticleSystem => _particleSystem;
        public Vector3 Position => transform.position;
        public EnemyController EnemyController { get; private set; }

        public void Initialize(EnemyController enemyController)
        {
            EnemyController = enemyController;
        }

        public void RemoveController()
        {
            EnemyController = null;
        }
    }
}