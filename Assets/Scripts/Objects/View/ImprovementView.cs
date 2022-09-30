using System.Collections;
using UnityEngine;
using GameLogic;
using System;

namespace GameView
{
	[RequireComponent(typeof(Rigidbody))]
	public class ImprovementView : MonoBehaviour, IDisposable
	{
		private Rigidbody _rigidbody;
		public Rigidbody Rigidbody
		{
			get { return _rigidbody; }
		}

		public Improvement logic { get; private set; }

		public void Initialize(Improvement log, Vector3 position)
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
