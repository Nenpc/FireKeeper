using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SliderWithText : MonoBehaviour
	{
		[SerializeField] private Image staminaImage;
		[SerializeField] private TextMeshProUGUI staminaText;

		public void UpdateImage(float amount, float maxAmount)
		{
			staminaImage.rectTransform.localScale = new Vector3(amount / maxAmount, 1, 1);
			staminaText.text = amount.ToString("0") + "/" + maxAmount.ToString("0");
		}
	}
}
