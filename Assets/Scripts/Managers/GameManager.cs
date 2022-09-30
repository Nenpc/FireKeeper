using GameUI;
using GameView;
using UnityEngine;

namespace Managers
{
	public class GameManager : BaseGameManager
	{
		[SerializeField] private DifficultSetting difficultSetting;
		[SerializeField] private Player player;
		[SerializeField] private BonfireView bonfireView;
		[SerializeField] private EndGameUI endGame;
		[SerializeField] private CameraPosition cameraPosition;

		private GameLogic.Bonfire bonfireLogic;

		public override void Dispose()
		{
			GameLogic.Bonfire.FireGoOut -= EndGame;
			endGame.RestartGameAction -= RestartGame;
		}

		public override bool Initialize()
		{
			try
			{
				if (!player.Initialize())
					return false;

				bonfireLogic = new GameLogic.Bonfire(difficultSetting.difficult, difficultSetting.bonfireMaxLifetime);
				if (!bonfireView.Initialize(bonfireLogic))
					return false;

				//if (!cameraPosition.Initialize())
				//	return false;

				GameLogic.Bonfire.FireGoOut += EndGame;
				endGame.RestartGameAction += RestartGame;

				TimeManager.Instance.Continue();

				return true;
			}
			catch
			{
				return false;
			}
		}

		public override string ManagerName()
		{
			return "Game";
		}

		private void EndGame()
		{
			TimeManager.Instance.Stop();
			endGame.ShowScreen();
		}

		private void RestartGame()
		{
		}
	}
}
