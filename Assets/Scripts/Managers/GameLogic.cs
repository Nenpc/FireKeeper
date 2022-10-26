using GameLogic;
using GameUI;
using GameView;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class GameLogic : MonoBehaviour
	{
		[SerializeField] private DifficultSetting difficultSetting;
		[SerializeField] private EndGameUI endGame;
		[SerializeField] private CameraPosition cameraPosition;

		private Bonfire bonfireLogic;
		private TimeManager timeManager;

		[Inject]
		private void Construct(TimeManager timeManager, Bonfire bonfire)
		{
			this.timeManager = timeManager;
			this.bonfireLogic = bonfire;

			//if (!cameraPosition.Initialize())
			//	return false;

			this.bonfireLogic.FireGoOut += EndGame;
			endGame.RestartGameAction += RestartGame;

			timeManager.Continue();
		}

		private void OnDestroy()
		{
			this.bonfireLogic.FireGoOut -= EndGame;
			endGame.RestartGameAction -= RestartGame;
		}

		private void EndGame()
		{
			timeManager.Stop();
			endGame.ShowScreen();
		}

		private void RestartGame()
		{
		}
	}
}
