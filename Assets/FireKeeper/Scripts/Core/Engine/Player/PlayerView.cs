using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _playerView;
        [SerializeField] private InteractionCollider _interactionCollider;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _fuelPosition;
        
        public CharacterController CharacterController => _characterController;
        public InteractionCollider InteractionCollider => _interactionCollider;
        public Animator Animator => _animator;
        public Transform FuelPosition => _fuelPosition;
        public Vector3 Position => _characterController.transform.position;
        public Transform View => _playerView;
        public IPlayerController PlayerController { get; private set; }

        public void Initialize(IPlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}