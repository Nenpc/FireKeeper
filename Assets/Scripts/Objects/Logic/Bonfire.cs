using System;
using GameView;
using Managers;
using UnityEngine;

namespace GameLogic
{
    public interface IBonfire
    {
        void AddLog(float quality);
        event Action FireGoOutEvent;
        event Action<float> LifetimeEvent;
        Transform GetTransform();
        Vector3 GetStartPosition();
    }

    public class Bonfire : IDisposable, IBonfire, IEndGame
    {
        public event Action FireGoOutEvent;
        public event Action<float> LifetimeEvent;
        public event Action EndGameEvent;
        
        private int maxLifetime = 100;
        private float lifetime;

        // ���������� ���������
        private float _difficult = 1;

        private ITimeService timeService;

        private BonfireView bonfireView;

        public Vector3 StartPosition { get; private set; }

        public Vector3 GetStartPosition()
        {
            return StartPosition;
        }

        public Bonfire(int difficult, int maxLifetime, ITimeService timeService, Vector3 startPosition, BonfireView bonfireView)
        {
            this.timeService = timeService;
            this.bonfireView = bonfireView;
            this.timeService.TickingEvent += Second;
            StartPosition = startPosition;
            this.maxLifetime = maxLifetime; 
            lifetime = maxLifetime;
        }

        public void Dispose()
        {
            timeService.TickingEvent -= Second;
        }

        public void AddLog(float quality)
        {
            lifetime = Mathf.Clamp(lifetime + quality, 0, maxLifetime);
            LifetimeEvent?.Invoke(lifetime);
            bonfireView.BonfirePower(lifetime);
        }

        private void Second(int time)
        {
            lifetime -= _difficult;
            if (lifetime <= 0)
            {
                FireGoOutEvent?.Invoke();
                EndGameEvent?.Invoke();
                bonfireView.FireGoOut();
            }
            else
            {
                LifetimeEvent?.Invoke(lifetime);
                bonfireView.BonfirePower(lifetime);
            }
        }

        public Transform GetTransform()
        {
            return bonfireView.transform;
        }
    }
}