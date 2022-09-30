using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class TimeManager : BaseGameManager
	{
		public static TimeManager Instance;

		public Action<int> Tiking;
		public Action StopAction;
		public Action ContinueAction;

		private float _gameTime;
		private float _lostTime;

		private bool active;
		private bool started;

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

		public override void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public override bool Initialize()
		{
			if (TimeManager.Instance != null)
			{
				return false;
			}

			TimeManager.Instance = this;
			started = true;
			_lostTime = 0;
			return true;
		}

		public override string ManagerName()
		{
			return "Time";
		}
	}
}
