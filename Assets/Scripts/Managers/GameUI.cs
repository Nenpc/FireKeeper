using System;
using GameLogic;
using UnityEngine;
using TMPro;
using GameView;
using GameUI;
using UI;
using Zenject;

namespace Managers
{
	public interface IGameUI
	{
		event Action ContinueEvent;
		event Action RestartEvent;
		event Action ReturnToMainMenuEvent;
		void ShowLosePanel();
		void ShowWinPanel(string sceneName);
	}

	public class GameUI : MonoBehaviour, IGameUI
	{
		public event Action ContinueEvent;
		public event Action RestartEvent;
		public event Action ReturnToMainMenuEvent;

		[SerializeField] private PlayerSetting playerSetting;
		[SerializeField] private DifficultSetting difficultSetting;

		[SerializeField] private SliderWithText staminaSlider;
		[SerializeField] private SliderWithText bonfireSlider;
		
		[SerializeField] private PauseMenuUI pauseMenu;
		[SerializeField] private LoseGameUI loseGame;
		[SerializeField] private WinMenuUI winGame;

		[Header("Interact")]
		[SerializeField] private TextMeshProUGUI interactText;

		private IPlayer player;
		private IBonfire bonfire;
		
		[Inject]
		private void Construct(IPlayer player, IBonfire bonfire)
		{
			this.player = player;
			this.bonfire = bonfire;
			
			this.player.StaminaChangedEvent += UpdateStaminaInfo;
			this.player.InteractObjectNearEvent += SeeInteract;

			this.bonfire.LifetimeEvent += UpdateBonfireInfo;
			
			pauseMenu.ContinueGameEvent += Continue;
			pauseMenu.MainMenuEvent += ReturnToMainMenu;
			
			loseGame.RestartGameEvent += Restart;
			loseGame.MainMenuEvent += ReturnToMainMenu;

			interactText.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			player.StaminaChangedEvent -= UpdateStaminaInfo;
			player.InteractObjectNearEvent -= SeeInteract;
			
			pauseMenu.ContinueGameEvent -= Continue;
			pauseMenu.MainMenuEvent -= ReturnToMainMenu;
			
			loseGame.RestartGameEvent -= Restart;
			loseGame.MainMenuEvent -= ReturnToMainMenu;
			
			bonfire.LifetimeEvent -= UpdateBonfireInfo;
		}

		private void Continue()
		{
		}
		
		private void ReturnToMainMenu()
		{
		}
		
		private void Restart()
		{
		}
		
		public void ShowLosePanel()
		{
			loseGame.ShowScreen();
		}
		
		public void ShowWinPanel(string sceneName)
		{
			winGame.ShowScreen(sceneName);
		}

		private void UpdateStaminaInfo(float amount)
		{
			staminaSlider.UpdateImage(amount, playerSetting.maxStamina);
		}

		private void UpdateBonfireInfo(float amount)
		{
			bonfireSlider.UpdateImage(amount, difficultSetting.bonfireMaxLifetime);
		}

		private void SeeInteract(bool see)
		{
			interactText.gameObject.SetActive(see);
		}
	}
}
