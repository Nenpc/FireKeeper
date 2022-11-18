using System;
using UnityEngine;
using Zenject;

namespace Managers
{
	public interface ITimeService
	{
		event Action<int> TickingEvent;
		event Action StopEvent;
		event Action ContinueEvent;
		void Stop();
		void Continue();
	}

	public class TimeService : MonoBehaviour, ITimeService
	{
		public event Action<int> TickingEvent;
		public event Action StopEvent;
		public event Action ContinueEvent;

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
			StopEvent?.Invoke();
			active = false;
		}

		public void Continue()
		{
			if (!started)
			{
				Debug.LogWarning("TimeManager manager is not running!");
				return;
			}
			ContinueEvent?.Invoke();
			active = true;
		}

		private void Update()
		{
			if (!started || !active) return;

			gameTime += Time.deltaTime;
			lostTime += Time.deltaTime;
			if (lostTime >= 1)
			{
				TickingEvent?.Invoke((int)gameTime);
				lostTime--;
			}
		}
	}
}
