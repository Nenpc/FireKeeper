using GameView;
using System;
using UnityEngine;

namespace GameLogic
{
	public enum ImprovementType
	{
		SpeedUp,
		StaminaRecovery,
		StaminaInfinite,
		NoStorm
	}

	public class Improvement : IDisposable
	{
		public Action<Improvement> ImprovementUsed;

		[SerializeField] public ImprovementType improvementType;

		public float Time { get; private set; }
		public float Capacity { get; private set; }
		public ImprovementView View { get; private set; }

		public Improvement(ImprovementType improvementType, float time, float capacity)
		{
			this.improvementType = improvementType;
			Time = time;
			Capacity = capacity;
		}

		public void AddView(ImprovementView view)
		{
			View = view;
		}

		public Improvement Use()
		{
			ImprovementUsed?.Invoke(this);
			return this;
		}

		public void SubtractTime()
		{
			Time--;
		}

		public void Dispose()
		{
			View = null;
		}
	}
}
