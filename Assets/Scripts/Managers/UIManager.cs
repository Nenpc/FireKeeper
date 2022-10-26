using GameLogic;
using UnityEngine;
using TMPro;
using GameView;
using UI;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private PlayerSetting playerSetting;
		[SerializeField] private DifficultSetting difficultSetting;

		[SerializeField] private SliderWithText staminaSlider;
		[SerializeField] private SliderWithText bonfireSlider;
		
		[Header("Menu")]
		[SerializeField] private GameObject menuPanel;
		[SerializeField] private Button continueButtonMenu;
		[SerializeField] private Button mainMenuButtonMenu;
		
		[Header("End game")]
		[SerializeField] private GameObject endGamePanel;
		[SerializeField] private Button restartButtonEndGame;
		[SerializeField] private Button mainMenuButtonEndGame;

		[Header("Interact")]
		[SerializeField] private TextMeshProUGUI interactText;

		private Player player;
		private Bonfire bonfire;
		
		[Inject]
		private void Construct(Player player, Bonfire bonfire)
		{
			this.player = player;
			this.bonfire = bonfire;

			
			this.player.staminaChanged += UpdateStaminaInfo;
			this.player.interactObjectNear += SeeInteract;

			this.bonfire.Lifetime += UpdateBonfireInfo;

			if (menuPanel != null || continueButtonMenu != null || mainMenuButtonMenu != null)
			{
				continueButtonMenu.onClick.AddListener(Continue);
				mainMenuButtonMenu.onClick.AddListener(ReturnToMainMenu);
			}
			
			if (endGamePanel != null || restartButtonEndGame != null || mainMenuButtonEndGame != null)
			{
				restartButtonEndGame.onClick.AddListener(Restart);
				mainMenuButtonEndGame.onClick.AddListener(ReturnToMainMenu);
			}
			interactText.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			player.staminaChanged -= UpdateStaminaInfo;
			player.interactObjectNear -= SeeInteract;
			
			continueButtonMenu.onClick.RemoveListener(Continue);
			mainMenuButtonMenu.onClick.RemoveListener(ReturnToMainMenu);

			restartButtonEndGame.onClick.RemoveListener(Restart);
			mainMenuButtonEndGame.onClick.RemoveListener(ReturnToMainMenu);
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
