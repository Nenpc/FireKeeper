using System;
using System.Collections.Generic;
using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerEffectController : IDisposable
    {
        public event Action<EffectTimer> AddEffectAction;
        public event Action<EffectTimer> TimeUpdateEffectAction;
        public event Action<EffectTimer> RemoveEffectAction;
        
        private readonly ICoreTimeController _coreTimeController;
        private readonly PlayerController _playerController;
        private readonly List<EffectTimer> _effectTimers;

        public PlayerEffectController(ICoreTimeController coreTimeController, PlayerController playerController)
        {
            _coreTimeController = coreTimeController;
            _playerController = playerController;
            _effectTimers = new List<EffectTimer>();

            _coreTimeController.TickAction += UpdateAllEffects;
        }
        
        public void Dispose()
        {
            _effectTimers.Clear();
            _coreTimeController.TickAction -= UpdateAllEffects;
        }

        public void ApplyEffects(IEffect effect)
        {
            effect.ApplyEffect(_playerController.GetPlayerParameters());
            var effectTimer = new EffectTimer(effect);
            _effectTimers.Add(effectTimer);
            AddEffectAction?.Invoke(effectTimer);
        }

        private void UpdateAllEffects(float deltaTime)
        {
            for (int i = 0; i < _effectTimers.Count; i++)
            {
                if (_effectTimers[i].ReduceTime(deltaTime))
                {
                    RemoveEffect(_effectTimers[i]);
                    _effectTimers.RemoveAt(i);
                    i--;
                }
                else
                {
                    TimeUpdateEffectAction?.Invoke(_effectTimers[i]);                 
                }
            }
        }

        private void RemoveEffect(EffectTimer effectTimer)
        {
            var effect = effectTimer.GetEffect();
            effect.UndoEffect(_playerController.GetPlayerParameters());
            RemoveEffectAction?.Invoke(effectTimer);
        }
    }
}