using System;
using UnityEngine;
using Zenject;

namespace Managers
{
	public interface ITimeService
	{
		void TickingSubscribe(Action<int> function);
		void TickingUnsubscribe(Action<int> function);
		void StopSubscribe(Action function);
		void StopUnsubscribe(Action function);
		void ContinueSubscribe(Action function);
		void ContinueUnsubscribe(Action function);
		void Stop();
		void Continue();
	}

	public class TimeService : MonoBehaviour, ITimeService
	{
		private Action<int> tickingAction;
		public void TickingSubscribe(Action<int> function) => tickingAction += function;
		public void TickingUnsubscribe(Action<int> function) => tickingAction -= function;

		private Action stopAction;
		public void StopSubscribe(Action function) => stopAction += function;
		public void StopUnsubscribe(Action function) => stopAction -= function;
		
		private Action continueAction;
		public void ContinueSubscribe(Action function) => continueAction += function;
		public void ContinueUnsubscribe(Action function) => continueAction -= function;

		private float gameTime;
		private float lostTime;

		private bool active;
		private bool started;
		
		[Inject]
		public void Construct()
		{
			started = true;
			lostTime = 0;
		}

		public void Stop()
		{
			if (!started)
			{
				Debug.LogWarning("TimeManager manager is not running!");
				return;
			}
			stopAction?.Invoke();
			active = false;
		}

		public void Continue()
		{
			if (!started)
			{
				Debug.LogWarning("TimeManager manager is not running!");
				return;
			}
			continueAction?.Invoke();
			active = true;
		}

		private void Update()
		{
			if (!started || !active) return;

			gameTime += Time.deltaTime;
			lostTime += Time.deltaTime;
			if (lostTime >= 1)
			{
				tickingAction?.Invoke((int)gameTime);
				lostTime--;
			}
		}
	}
}
