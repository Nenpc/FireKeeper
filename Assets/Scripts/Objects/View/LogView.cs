using System;
using UnityEngine;
using GameLogic;

namespace GameView
{
	[RequireComponent (typeof(Rigidbody))]
	public class LogView : MonoBehaviour, IDisposable
	{
		private Rigidbody _rigidbody;
		public Rigidbody Rigidbody
		{
			get { return _rigidbody; }
		}
	
		public Log logic { get; private set; }

		public bool IsTake => logic.IsTake;

		public void Initialize(Log log, Vector3 position)
		{
			logic = log;
			logic.AddView(this);
			transform.position = position;
			gameObject.SetActive(true);
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Dispose()
		{
			logic = null;
		}
	}
}
