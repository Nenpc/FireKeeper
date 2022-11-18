using System;
using GameView;
using Managers;
using UnityEngine;

namespace GameLogic
{
    public interface IBonfire
    {
        void AddLog(float quality);
        void FireGoOutSubscribe(Action function);
        void FireGoOutUnsubscribe(Action function);
        void LifetimeSubscribe(Action<float> function);
        void LifetimeUnsubscribe(Action<float> function);
        Transform GetTransform();
        Vector3 GetStartPosition();
    }

    public class Bonfire : IDisposable, IBonfire, IEndGame
    {
        private Action fireGoOutAction;
        public void FireGoOutSubscribe(Action function) => fireGoOutAction += function;
        public void FireGoOutUnsubscribe(Action function) => fireGoOutAction -= function;

        private Action<float> lifetimeAction;
        public void LifetimeSubscribe(Action<float> function) => lifetimeAction += function;
        public void LifetimeUnsubscribe(Action<float> function) => lifetimeAction -= function;
        
        private Action endGameAction;
        public void EndGameSubscribe(Action function) => endGameAction += function;
        public void EndGameUnsubscribe(Action function) => endGameAction -= function;

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
            this.timeService.TickingSubscribe(Second);
            StartPosition = startPosition;
            this.maxLifetime = maxLifetime; 
            lifetime = maxLifetime;
        }

        public void Dispose()
        {
            timeService.TickingUnsubscribe(Second);
        }

        public void AddLog(float quality)
        {
            lifetime = Mathf.Clamp(lifetime + quality, 0, maxLifetime);
            lifetimeAction?.Invoke(lifetime);
            bonfireView.BonfirePower(lifetime);
        }

        private void Second(int time)
        {
            lifetime -= _difficult;
            if (lifetime <= 0)
            {
                fireGoOutAction?.Invoke();
                endGameAction?.Invoke();
                bonfireView.FireGoOut();
            }
            else
            {
                lifetimeAction?.Invoke(lifetime);
                bonfireView.BonfirePower(lifetime);
            }
        }

        public Transform GetTransform()
        {
            return bonfireView.transform;
        }
    }
}