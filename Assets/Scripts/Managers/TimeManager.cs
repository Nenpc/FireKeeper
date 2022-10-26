using System;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class TimeManager : MonoBehaviour
	{
		public Action<int> Tiking;
		public Action StopAction;
		public Action ContinueAction;

		private float _gameTime;
		private float _lostTime;

		private bool active;
		private bool started;
		
		[Inject]
		public void Construct()
		{
			started = true;
			_lostTime = 0;
		}

		public void Stop()
		{
			if (!started)
			{
				Debug.LogWarning("TimeManager manager is not running!");
				return;
			}
			StopAction?.Invoke();
			active = false;
		}

		public void Continue()
		{
			if (!started)
			{
				Debug.LogWarning("TimeManager manager is not running!");
				return;
			}
			ContinueAction?.Invoke();
			active = true;
		}

		private void Update()
		{
			if (!started || !active) return;

			_gameTime += Time.deltaTime;
			_lostTime += Time.deltaTime;
			if (_lostTime >= 1)
			{
				Tiking?.Invoke((int)_gameTime);
				_lostTime--;
			}
		}
	}
}
