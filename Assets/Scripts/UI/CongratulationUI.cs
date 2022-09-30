using System.Collections;
using TMPro;
using UnityEngine;

namespace GameUI
{
	public class CongratulationUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI congratulationText;
		[SerializeField] private int showPanelTime = 10;

		public void ShowPanel(string text)
		{
			congratulationText.text = text;
			gameObject.SetActive(true);
			StartCoroutine(nameof(HidePane));
		}

		public IEnumerator HidePane()
		{
			yield return new WaitForSeconds(showPanelTime);
			gameObject.SetActive(false);
		}
	}
}
