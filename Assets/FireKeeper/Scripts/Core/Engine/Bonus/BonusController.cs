using System;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonusController : IDisposable
    {
        private IBonusDefinition _definition;
        private readonly IEffect _effect;
        private readonly IBonusFactory _bonusFactory;
        private readonly BonusView _view;

        public IBonusDefinition Definition => _definition;
        public BonusView GetView() => _view;

        public BonusController(IBonusDefinition definition, 
            IBonusFactory bonusFactory,
            BonusView view)
        {
            _definition = definition;
            _bonusFactory = bonusFactory;
            _view = view;

            _view.OnTriggerEnterAction += OnTriggerEnterAction;
            _effect = _definition.EffectDefinition.GetEffect();
        }
        
        public void Dispose()
        {
            _view.OnTriggerEnterAction -= OnTriggerEnterAction;
        }

        private void OnTriggerEnterAction(Collider collider)
        {
            if (collider.TryGetComponent<PlayerView>(out var playerView))
            {
                playerView.PlayerController.ApplyEffect(_effect);
                _bonusFactory.Destroy(_view);
            }
        }
    }
}