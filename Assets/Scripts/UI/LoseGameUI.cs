using UnityEngine;
using UnityEngine.UI;
using System;

namespace GameUI
{
	public class LoseGameUI : MonoBehaviour
	{
		public event Action RestartGameEvent;
		public event Action MainMenuEvent;

		[SerializeField] private Button restart;

		private void Awake()
		{
			restart.onClick.AddListener(RestartGame);
		}

		private void OnDestroy()
		{
			restart.onClick.RemoveListener(RestartGame);
		}

		public void ShowScreen()
		{
			gameObject.SetActive(true);
		}

		public void HideScreen()
		{
			gameObject.SetActive(false);
		}

		private void RestartGame()
		{
			RestartGameEvent?.Invoke();
		}
		
		private void MainMenu()
		{
			MainMenuEvent?.Invoke();
		}
	}
}
