using UnityEngine;
using System;
using Managers;

namespace GameLogic
{
	public class Bonfire : IDisposable
	{
		public static Action FireGoOut;
		public static Action<float> Lifetime;

		private int maxLifetime = 100;
		private float _lifetime;

		// Прикрутить сложность
		private float _difficult = 1;

		public Bonfire(int difficult, int maxLifetime)
		{
			TimeManager.Instance.Tiking += Second;
			_lifetime = maxLifetime;
		}

		public void Dispose()
		{
			TimeManager.Instance.Tiking -= Second;
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
