using UnityEngine;
using UnityEngine.UI;
using System;

namespace GameUI
{
	public class EndGameUI : MonoBehaviour
	{
		public Action RestartGameAction;

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
			RestartGameAction?.Invoke();
		}
	}
}
