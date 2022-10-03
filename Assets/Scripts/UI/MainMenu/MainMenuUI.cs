using UnityEngine;
using UnityEngine.UI;
using AdditionalFunctions;

namespace MainMenu
{
	public class MainMenuUI : MonoBehaviour
	{
		[SerializeField] private Button startGameButton;
		[SerializeField] private Button leaderboardButton;
		[SerializeField] private Button quiteButton;
		[SerializeField] private GameObject leaderboardPanel;
		[SerializeField] private LevelPanel levelPanel;

		[SerializeField] private Managers.SceneManager sceneManager;

		private void Awake()
		{
			if (startGameButton != null)
				startGameButton.onClick.AddListener(ShowLevelPanel);
			else
				Debug.LogError("Initialization error start button not set!");

			if (leaderboardButton != null)
				leaderboardButton.onClick.AddListener(LeaderboardClick);
			else
				Debug.LogError("Initialization error leaderboard button not set!");

			if (quiteButton != null)
				quiteButton.onClick.AddListener(QuiteClick);
			else
				Debug.LogError("Initialization error quite button not set!");

			if (sceneManager == null)
				Debug.LogError("Initialization error scene manager setting not set!");
			
			if (levelPanel == null)
				Debug.LogError("Initialization error level panel not set!");
		}

		private void ShowLevelPanel()
		{
			levelPanel.gameObject.SetActive(true);
		}

		private void LeaderboardClick()
		{
			leaderboardPanel.SetActive(true);
		}

		private void QuiteClick()
		{
			AlertSystem.Instance.ShowMessageOkCancel(
				"Are you sure you want to exit the game?", "Ok", "Cancel", QuitGame, null);
		}

		private void QuitGame()
		{
			Application.Quit();
		}

		private void OnDestroy()
		{
			if (startGameButton != null)
				startGameButton.onClick.RemoveListener(ShowLevelPanel);

			if (leaderboardButton != null)
				leaderboardButton.onClick.RemoveListener(LeaderboardClick);

			if (quiteButton != null)
				quiteButton.onClick.RemoveListener(QuiteClick);
		}
	}
}
