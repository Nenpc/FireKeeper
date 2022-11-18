using System;
using UnityEngine;
using Zenject;

namespace Managers
{
	interface IInputManager
	{
		
	}
	
	public class InputManager : MonoBehaviour, IInputManager
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

		private ITimeService timeService;
		
		[Inject]
		private void Construct(ITimeService timeService)
		{
			this.timeService = timeService;
			
			timeService.StopSubscribe(StopGame);
			timeService.ContinueSubscribe(ContinueGame);
		}

		public void OnDestroy()
		{
			timeService.StopUnsubscribe(StopGame);
			timeService.ContinueUnsubscribe(ContinueGame);
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
