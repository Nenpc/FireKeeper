using UnityEngine;
using UnityEngine.UI;
using AdditionalFunctions;

namespace MainMenu
{
	public class MainMenuUI : MonoBehaviour
	{
		[SerializeField] private Button startGameButton;
		[SerializeField] private Button loadLastGameButton;
		[SerializeField] private Button leaderboardButton;
		[SerializeField] private Button quiteButton;
		[SerializeField] private GameObject leaderboardPanel;

		[SerializeField] private Managers.SceneManager sceneManager;

		private void Awake()
		{
			if (startGameButton != null)
				startGameButton.onClick.AddListener(StartGameClick);
			else
				Debug.LogError("Initialization error start button not set!");

			if (loadLastGameButton != null)
				loadLastGameButton.onClick.AddListener(LoadLastGameClick);
			else
				Debug.LogError("Initialization error load last game button not set!");

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
		}

		private void StartGameClick()
		{
			sceneManager.LoadScene(SceneName.SummerLevel);
		}

		private void LoadLastGameClick()
		{
			sceneManager.LoadScene(SceneName.SummerLevel, "Load");
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
				startGameButton.onClick.RemoveListener(StartGameClick);

			if (loadLastGameButton != null)
				loadLastGameButton.onClick.RemoveListener(LoadLastGameClick);

			if (leaderboardButton != null)
				leaderboardButton.onClick.RemoveListener(LeaderboardClick);

			if (quiteButton != null)
				quiteButton.onClick.RemoveListener(QuiteClick);
		}
	}
}
