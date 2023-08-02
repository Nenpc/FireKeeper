using System;
using System.Collections.Generic;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerController : IPlayerController, IDisposable
    {
        public event Action<Collider> FindedObjectAction;
        public event Action<Collider> LosedObjectAction;
        
        public event Action<EffectTimer> AddEffectAction;
        public event Action<EffectTimer> TimeUpdateEffectAction;
        public event Action<EffectTimer> RemoveEffectAction;
        
        public Vector3 Position => _view.Position;
        public Transform FuelPosition => _view.FuelPosition;

        private readonly PlayerParameters _playerParameters;
        private readonly ICoreTimeController _coreTimeController;
        private readonly PlayerEffectController _playerEffectController;
        private readonly IPlayerInteractionController _playerInteractionController;

        private PlayerView _view;

        public PlayerParameters GetPlayerParameters() => _playerParameters;

        public PlayerController(IPlayerConfig playerConfig, 
            ICoreTimeController coreTimeController)
        {
            _playerParameters = new PlayerParameters(playerConfig.GetDefinition());
            _coreTimeController = coreTimeController;
            _playerEffectController = new PlayerEffectController(_coreTimeController, this);
            _playerInteractionController = new PlayerInteractionController(this);

            _coreTimeController.TickAction += Tick;
            _playerEffectController.AddEffectAction += Add;
            _playerEffectController.TimeUpdateEffectAction += TimeUpdate;
            _playerEffectController.RemoveEffectAction += Remove;
        }

        private void Add(EffectTimer effect) => AddEffectAction?.Invoke(effect);
        private void TimeUpdate(EffectTimer effect) => TimeUpdateEffectAction?.Invoke(effect);
        private void Remove(EffectTimer effect) => RemoveEffectAction?.Invoke(effect);

        public void Dispose()
        {
            if (_view != null)
            {
                _view.InteractionCollider.FindedObjectAction -= FindObject;
                _view.InteractionCollider.LosedObjectAction -= LosedObject;
                _view = null;
            }
            
            _coreTimeController.TickAction -= Tick;
            _playerEffectController.AddEffectAction -= Add;
            _playerEffectController.TimeUpdateEffectAction -= TimeUpdate;
            _playerEffectController.RemoveEffectAction -= Remove;
        }

        public void UpdateView(PlayerView view)
        {
            if (_view != null)
            {
                _view.InteractionCollider.FindedObjectAction -= FindObject;
                _view.InteractionCollider.LosedObjectAction -= LosedObject;
            }

            _view = view;
            _view.InteractionCollider.FindedObjectAction += FindObject;
            _view.InteractionCollider.LosedObjectAction += LosedObject;
        }

        private void FindObject(Collider collider) => FindedObjectAction?.Invoke(collider);
        private void LosedObject(Collider collider) => LosedObjectAction?.Invoke(collider);

        public void ApplyEffect(IEffect effect)
        {
            _playerEffectController.ApplyEffects(effect);
        }

        private void Tick(float deltaTime)
        {
            if (_view == null) Debug.LogError("No player view!");

            Move(deltaTime);
            Interact();
        }

        private void Move(float deltaTime)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var speed = _playerParameters.GetStepSpeed() * deltaTime;
            var direction = new Vector3(horizontal,0, vertical);
            direction = direction.normalized * speed;

            if (direction.sqrMagnitude > 0)
            {
                _view.CharacterController.Move(new Vector3(direction.x, 0, direction.z));
                _view.Animator.SetBool("Run", true);
            }
            else
            {
                _view.Animator.SetBool("Run", false);
            }
            
            _view.View.transform.LookAt(_view.Position + direction);
        }

        private void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerInteractionController.Interact();
            }
        }
    }
}