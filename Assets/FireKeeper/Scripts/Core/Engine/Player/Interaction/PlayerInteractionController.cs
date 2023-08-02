using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerInteractionController : IDisposable, IPlayerInteractionController
    {
        private readonly IPlayerController _playerController;
        private readonly Dictionary<Type,IInteractionState> _interactionStates;

        private InteractionMono _nearestObject;
        private IInteractionState _currentInteractionState;
        private InteractionMono _pickedUpObject;

        public InteractionMono PickedUpObject
        {
            get { return _pickedUpObject; }
            set
            {
                if (value == null || _pickedUpObject == null)
                {
                    if (value != null)
                    {
                        value.transform.SetParent(_playerController.FuelPosition);
                        value.transform.localPosition = Vector3.zero;
                        value.transform.localRotation = Quaternion.identity;
                    }

                    _pickedUpObject = value;
                }
            }
        }

        public PlayerInteractionController(IPlayerController playerController)
        {
            _playerController = playerController;

            _playerController.FindedObjectAction += FindObject;
            _playerController.LosedObjectAction += LoseObject;

            _interactionStates = new Dictionary<Type, IInteractionState>()
            {
                {typeof(TakeFuelState),new TakeFuelState(this)},
                {typeof(InteractionState),new InteractionState(this)},
                {typeof(BurnFuelState),new BurnFuelState(this)}
            };
        }

        private void FindObject(Collider collider)
        {
            if (collider.TryGetComponent<InteractionMono>(out var interactionMono))
            {
                _nearestObject = interactionMono;
                _currentInteractionState = _interactionStates[interactionMono.GetInteractionType()];
                var t = interactionMono.GetInteractionInfo().GetType();
            }
        }

        private void LoseObject(Collider collider)
        {
            if (collider.TryGetComponent<InteractionMono>(out var interactionMono) && _nearestObject == interactionMono)
            {
                _nearestObject = null;
                _currentInteractionState = null;
            }
        }

        public bool HasObject() => _pickedUpObject != null;

        public void Interact()
        {
            if (_currentInteractionState != null)
                _currentInteractionState.Interact(_nearestObject);
        }

        public void Dispose()
        {
            _playerController.FindedObjectAction -= FindObject;
            _playerController.LosedObjectAction -= LoseObject;
            
            _nearestObject = null;
            _currentInteractionState = null;
        }
    }
}