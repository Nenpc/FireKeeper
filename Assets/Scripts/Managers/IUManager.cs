using UnityEngine;
using TMPro;
using GameView;
using UI;
using UnityEngine.UI;

namespace Managers
{
	public class IUManager : BaseGameManager
	{
		[SerializeField] private PlayerSetting playerSetting;
		[SerializeField] private DifficultSetting difficultSetting;

		[SerializeField] private SliderWithText stamina;
		[SerializeField] private SliderWithText bonefire;
		
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

		public override void Dispose()
		{
			Player.staminaChanged -= UpdateStaminaInfo;
			Player.interactObjectNear -= SeeInteract;
			
			continueButtonMenu.onClick.RemoveListener(Continue);
			mainMenuButtonMenu.onClick.RemoveListener(ReturnToMainMenu);

			restartButtonEndGame.onClick.RemoveListener(Restart);
			mainMenuButtonEndGame.onClick.RemoveListener(ReturnToMainMenu);
		}

		public override bool Initialize()
		{
			try
			{
				Player.staminaChanged += UpdateStaminaInfo;
				Player.interactObjectNear += SeeInteract;

				GameLogic.Bonfire.Lifetime += UpdateBonfireInfo;

				if (stamina == null)
					return false;

				if (interactText == null)
					return false;

				if (menuPanel != null || continueButtonMenu != null || mainMenuButtonMenu != null)
				{
					continueButtonMenu.onClick.AddListener(Continue);
					mainMenuButtonMenu.onClick.AddListener(ReturnToMainMenu);
				}
				else
				{
					return false;
				}
				
				if (endGamePanel != null || restartButtonEndGame != null || mainMenuButtonEndGame != null)
				{
					restartButtonEndGame.onClick.AddListener(Restart);
					mainMenuButtonEndGame.onClick.AddListener(ReturnToMainMenu);
				}
				else
				{
					return false;
				}

				interactText.gameObject.SetActive(false);

				return true;
			}
			catch
			{
				return false;
			}
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

		public override string ManagerName()
		{
			return "UIManager";
		}

		private void UpdateStaminaInfo(float amount)
		{
			stamina.UpdateImage(amount, playerSetting.maxStamina);
		}

		private void UpdateBonfireInfo(float amount)
		{
			bonefire.UpdateImage(amount, difficultSetting.bonfireMaxLifetime);
		}

		private void SeeInteract(bool see)
		{
			interactText.gameObject.SetActive(see);
		}
	}
}
