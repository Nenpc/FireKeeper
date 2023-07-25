using System;
using UnityEngine;
using Zenject;

namespace FireKeeper.Core.Engine
{
    public sealed class CoreTimeController : ICoreTimeController, ITickable
    {
        private bool _playe = false;
        private float _leftTime = 0;
        public event Action<float> TickAction;

        public void Stop() => _playe = false;
        public void Start() => _playe = true;

        public float GetLeftTime() => _leftTime;

        public void Tick()
        {
            if (_playe)
            {
                _leftTime += Time.deltaTime;
                TickAction?.Invoke(Time.deltaTime);
            }
        }
    }
}