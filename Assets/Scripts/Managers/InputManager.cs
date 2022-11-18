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

		public event Action<bool> GoFront;
		public event Action<bool> GoRight;
		public event Action<bool> GoLeft;
		public event Action<bool> GoBack;
		public event Action<bool> Sprint;

		public event Action Interaction;
		public event Action Drop;

		private bool wait;

		private ITimeService timeService;
		
		[Inject]
		private void Construct(ITimeService timeService)
		{
			this.timeService = timeService;
			
			timeService.StopEvent += StopGame;
			timeService.ContinueEvent += ContinueGame;
		}

		public void OnDestroy()
		{
			timeService.StopEvent -= StopGame;
			timeService.ContinueEvent -=ContinueGame;
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
