using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class InputManager : BaseGameManager
	{
		public static InputManager Instance;

		[SerializeField] private InputSetting inputSetting;

		public Action<bool> GoFront;
		public Action<bool> GoRight;
		public Action<bool> GoLeft;
		public Action<bool> GoBack;
		public Action<bool> Sprint;

		public Action Interaction;
		public Action Drop;

		private bool wait;

		public override void Dispose()
		{
			Instance = null;

			TimeManager.Instance.StopAction -= StopGame;
			TimeManager.Instance.ContinueAction -= ContinueGame;
		}

		private void StopGame()
		{
			wait = true;
		}

		private void ContinueGame()
		{
			wait = false;
		}

		public override bool Initialize()
		{
			try
			{
				if (Instance != null)
				{
					Debug.LogWarning("Input manager alredy inicialized");
					return false;
				}

				Instance = this;

				TimeManager.Instance.StopAction += StopGame;
				TimeManager.Instance.ContinueAction += ContinueGame;

				return true;
			}
			catch
			{
				Debug.LogWarning("Input manager Launch problems");
				return false;
			}
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

		public override string ManagerName()
		{
			return "Input manager";
		}
	}
}
