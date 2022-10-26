using System;
using Managers;
using UnityEngine;

namespace GameLogic
{
    public class Bonfire : IDisposable
    {
        public Action FireGoOut;
        public Action<float> Lifetime;

        private int maxLifetime = 100;
        private float _lifetime;

        // ���������� ���������
        private float _difficult = 1;

        private TimeManager timeManager;
	
        public Bonfire(int difficult, int maxLifetime, TimeManager timeManager)
        {
            this.timeManager = timeManager;
            this.timeManager.Tiking += Second;
            _lifetime = maxLifetime;
        }

        public void Dispose()
        {
            timeManager.Tiking -= Second;
        }

        public void AddLog(float quality)
        {
            _lifetime = Mathf.Clamp(_lifetime + quality, 0, maxLifetime);
            Lifetime?.Invoke(_lifetime);
        }

        private void Second(int time)
        {
            _lifetime -= _difficult;
            if (_lifetime <= 0)
            {
                FireGoOut?.Invoke();
            }
            else
            {
                Lifetime?.Invoke(_lifetime);
            }
        }
    }
}