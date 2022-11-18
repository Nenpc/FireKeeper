using GameLogic;
using GameView;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class GameLogic : MonoBehaviour
	{
		[SerializeField] private DifficultSetting difficultSetting;
		[SerializeField] private CameraPosition cameraPosition;
		
		[Space]
		[SerializeField] private Player playerPrefab;
		[SerializeField] private Transform playerStartPosition;
		[SerializeField] private BonfireView bonfireViewPrefab;
		[SerializeField] private Transform bonfireStartPosition;

		private IBonfire bonfire;
		private ITimeService timeService;
		private IGameUI gameUI;
		private IWin win;

		[Inject]
		private void Construct(ITimeService timeService, IWin win, IBonfire bonfire, IGameUI gameUI)
		{
			this.timeService = timeService;
			this.bonfire = bonfire;
			this.gameUI = gameUI;
			this.win = win;
			
			//if (!cameraPosition.Initialize())
			//	return false;

			this.bonfire.FireGoOutSubscribe(EndGame);
			this.gameUI.RestartSubscribe(RestartGame);
			this.gameUI.ContinueSubscribe(RestartGame);

			this.timeService.Continue();
			
			this.win.WinActionSubscribe(WinGame);
		}

		private void OnDestroy()
		{
			bonfire.FireGoOutUnsubscribe(EndGame);
			gameUI.RestartUnsubscribe(RestartGame);
			gameUI.ContinueUnsubscribe(RestartGame);
		}

		private void EndGame()
		{
			timeService.Stop();
			gameUI.ShowLosePanel();
		}

		private void WinGame(SceneName sceneName)
		{
			timeService.Stop();
			gameUI.ShowWinPanel(sceneName.ToString());
		}

		private void RestartGame()
		{
		}
	}
}
