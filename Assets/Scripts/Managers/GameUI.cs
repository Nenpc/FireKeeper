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
		void ContinueSubscribe(Action function);
		void ContinueUnsubscribe(Action function);
		void RestartSubscribe(Action function);
		void RestartUnsubscribe(Action function);
		void ReturnToMainMenuSubscribe(Action function);
		void ReturnToMainMenuUnsubscribe(Action function);
		void ShowLosePanel();
		void ShowWinPanel(string sceneName);
	}

	public class GameUI : MonoBehaviour, IGameUI
	{
		private Action continueAction;
		public void ContinueSubscribe(Action function) => continueAction += function;
		public void ContinueUnsubscribe(Action function) => continueAction -= function;

		private Action restartAction;
		public void RestartSubscribe(Action function) => restartAction += function;
		public void RestartUnsubscribe(Action function) => restartAction -= function;
		
		private Action returnToMainMenuAction;
		public void ReturnToMainMenuSubscribe(Action function) => returnToMainMenuAction += function;
		public void ReturnToMainMenuUnsubscribe(Action function) => returnToMainMenuAction -= function;
		
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
			
			this.player.StaminaChangedSubscribe(UpdateStaminaInfo);
			this.player.InteractObjectNearSubscribe(SeeInteract);

			this.bonfire.LifetimeSubscribe(UpdateBonfireInfo);
			
			pauseMenu.ContinueGameActionSubscribe(Continue);
			pauseMenu.MainMenuActionSubscribe(ReturnToMainMenu);
			
			loseGame.RestartGameActionSubscribe(Restart);
			loseGame.MainMenuActionSubscribe(ReturnToMainMenu);

			interactText.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			player.StaminaChangedUnsubscribe(UpdateStaminaInfo);
			player.InteractObjectNearUnsubscribe(SeeInteract);
			
			pauseMenu.ContinueGameActionUnsubscribe(Continue);
			pauseMenu.MainMenuActionUnsubscribe(ReturnToMainMenu);
			
			loseGame.RestartGameActionUnsubscribe(Restart);
			loseGame.MainMenuActionUnsubscribe(ReturnToMainMenu);
			
			bonfire.LifetimeUnsubscribe(UpdateBonfireInfo);
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
