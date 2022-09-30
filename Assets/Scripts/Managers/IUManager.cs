using UnityEngine;
using TMPro;
using GameView;
using UI;

namespace Managers
{
	public class IUManager : BaseGameManager
	{
		[SerializeField] private InputSetting inputSetting;
		[SerializeField] private PlayerSetting playerSetting;
		[SerializeField] private DifficultSetting difficultSetting;

		[SerializeField] private SliderWithText stamina;
		[SerializeField] private SliderWithText bonefire;

		[Header("Interact")]
		[SerializeField] private TextMeshProUGUI interactText;

		public override void Dispose()
		{
			Player.staminaChanged -= UpdateStaminaInfo;
			Player.interactObjectNear -= SeeInteract;
		}

		public override bool Initialize()
		{
			try
			{
				Player.staminaChanged += UpdateStaminaInfo;
				Player.interactObjectNear += SeeInteract;

				GameLogic.Bonfire.Lifetime += UpdateBonefireInfo;

				if (inputSetting == null)
					 return false;

				if (stamina == null)
					return false;

				if (interactText == null)
					return false;

				interactText.gameObject.SetActive(false);

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ManagerName()
		{
			return "UIManager";
		}

		private void UpdateStaminaInfo(float amount)
		{
			stamina.UpdateImage(amount, playerSetting.maxStamina);
		}

		private void UpdateBonefireInfo(float amount)
		{
			bonefire.UpdateImage(amount, difficultSetting.bonfireMaxLifetime);
		}

		private void SeeInteract(bool see)
		{
			interactText.gameObject.SetActive(see);
		}
	}
}
