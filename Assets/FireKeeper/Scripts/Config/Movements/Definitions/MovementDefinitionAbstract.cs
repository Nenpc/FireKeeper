using UnityEngine;

namespace FireKeeper.Config
{
    public abstract class MovementDefinitionAbstract : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
        
        public abstract IMovement GetMovement();
    }
}