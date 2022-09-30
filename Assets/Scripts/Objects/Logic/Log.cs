using Managers;
using System;
using UnityEngine;
using GameView;

namespace GameLogic
{
	public class Log : IDisposable
	{
		public Action<Log> LogBurned;

		public LogView view { get; private set;}

		public Transform transform => view.transform;

		public LogSize logSize;
		public float logSlowdown { get; private set; }
		public float logCapacity { get; private set; }

		public bool IsTake{ get; private set; }

		public Log(LogSize logSize, float logSlowdown, float logCapacity)
		{
			this.logSize = logSize;
			this.logSlowdown = logSlowdown;
			this.logCapacity = logCapacity;
			IsTake = false;
		}

		public void AddView(LogView view)
		{
			this.view = view;
		}

		public bool Take(out Log log)
		{
			if (!IsTake)
			{
				IsTake = true;
				view.Rigidbody.isKinematic = true;
				log = this;
				return true;
			}
			else
			{
				log = null;
				return false;
			}
		}

		public bool Drop()
		{
			if (IsTake)
			{
				view.transform.SetParent(null);
				view.Rigidbody.isKinematic = false;
				IsTake = false;
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Burn()
		{
			LogBurned?.Invoke(this);
		}

		public void Dispose()
		{
			view = null;
		}
	}
}
