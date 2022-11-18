using UnityEngine;
using UnityEngine.UI;
using System;

namespace GameUI
{
	public class LoseGameUI : MonoBehaviour
	{
		private Action restartGameAction;
		public void RestartGameActionSubscribe(Action function) => restartGameAction += function;
		public void RestartGameActionUnsubscribe(Action function) => restartGameAction -= function;
		
		private Action mainMenuAction;
		public void MainMenuActionSubscribe(Action function) => mainMenuAction += function;
		public void MainMenuActionUnsubscribe(Action function) => mainMenuAction -= function;

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
			restartGameAction?.Invoke();
		}
		
		private void MainMenu()
		{
			mainMenuAction?.Invoke();
		}
	}
}
