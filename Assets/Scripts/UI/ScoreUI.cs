using TMPro;
using UnityEngine;

namespace GameUI
{
	public class ScoreUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI scoreText;

		public void ShowScore(int score)
		{
			scoreText.text = score.ToString();
		}
	}
}
