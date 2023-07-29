using System;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonfireController : IBonfireController, IDisposable
    {
        public event Action GoOutAction;

        private readonly ICoreTimeController _coreTimeController;
        private readonly IBonfireDefinition _definition;
        
        private float _lifeTime;
        private BonfireView _view;
        
        public IBonfireDefinition Definition => _definition;
        
        public BonfireController(ICoreTimeController coreTimeController, 
            IBonfireConfig config)
        {
            _definition = config.GetDefinition();
            _coreTimeController = coreTimeController;

            _lifeTime = _definition.MaxLife;
            _coreTimeController.TickAction += Tick;
        }
        
        public void UpdateView(BonfireView view)
        {
            _view = view;
        }
        
        public void Dispose()
        {
            _coreTimeController.TickAction += Tick;
        }

        private void Tick(float deltaTime)
        {
            _lifeTime = Mathf.Clamp(_lifeTime - _definition.FadingPerSecond * deltaTime, 0, _definition.MaxLife);
            _view?.BonfirePower(_lifeTime / _definition.MaxLife);
            
            if (_lifeTime == 0)
                GoOutAction?.Invoke();
        }

        public void AddLog(float quality)
        {
            _lifeTime = Mathf.Clamp(_lifeTime + quality, 0, _definition.MaxLife);
            _view?.BonfirePower(_lifeTime / _definition.MaxLife);
        }
    }
}