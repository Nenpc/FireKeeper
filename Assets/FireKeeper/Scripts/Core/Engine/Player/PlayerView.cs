using System;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    [RequireComponent(typeof(CharacterController), typeof(Transform))]
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private InteractionCollider _interactionCollider;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _fuelPosition;
        
        public CharacterController CharacterController => _characterController;
        public InteractionCollider InteractionCollider => _interactionCollider;
        public Animator Animator => _animator;
        public Transform FuelPosition => _fuelPosition;
        public Vector3 Position => transform.position;
        public IPlayerController PlayerController { get; private set; }

        public void Initialize(IPlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}