using System;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class InputManager : MonoBehaviour
	{
		[SerializeField] private InputSetting inputSetting;

		public Action<bool> GoFront;
		public Action<bool> GoRight;
		public Action<bool> GoLeft;
		public Action<bool> GoBack;
		public Action<bool> Sprint;

		public Action Interaction;
		public Action Drop;

		private bool wait;

		private TimeManager timeManager;
		
		[Inject]
		private void Construct(TimeManager timeManager)
		{
			this.timeManager = timeManager;
			
			timeManager.StopAction += StopGame;
			timeManager.ContinueAction += ContinueGame;
		}

		public void OnDestroy()
		{
			timeManager.StopAction -= StopGame;
			timeManager.ContinueAction -= ContinueGame;
		}

		private void StopGame()
		{
			wait = true;
		}

		private void ContinueGame()
		{
			wait = false;
		}

		private void Update()
		{
			if (wait)
				return;

			if (Input.GetKey(inputSetting.Forward))
			{
				GoFront?.Invoke(true);
			}
			else
			{
				GoFront?.Invoke(false);
			}

			if (Input.GetKey(inputSetting.Right))
			{
				GoRight?.Invoke(true);
			}
			else
			{
				GoRight?.Invoke(false);
			}

			if (Input.GetKey(inputSetting.Left))
			{
				GoLeft?.Invoke(true);
			}
			else
			{
				GoLeft?.Invoke(false);
			}

			if (Input.GetKey(inputSetting.Back))
			{
				GoBack?.Invoke(true);
			}
			else
			{
				GoBack?.Invoke(false);
			}

			if (Input.GetKey(inputSetting.Run))
			{
				Sprint?.Invoke(true);
			}
			else
			{
				Sprint?.Invoke(false);
			}

			if (Input.GetKeyDown(inputSetting.Interaction))
			{
				Interaction();
			}

			if (Input.GetKeyDown(inputSetting.Drop))
			{
				Drop();
			}
		}
	}
}
