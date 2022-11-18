using System;
using GameUI;
using UnityEngine;
using Zenject;

namespace Managers
{
	interface IProgressManager
	{
	}

	interface IWin
	{
		void WinActionSubscribe(Action<SceneName> function);
		void WinActionUnsubscribe(Action<SceneName> function);
	}
	
	public class ProgressManager : MonoBehaviour, IProgressManager, IWin
	{
		private Action<SceneName> winAction;
		public void WinActionSubscribe(Action<SceneName> function) => winAction += function;
		public void WinActionUnsubscribe(Action<SceneName> function) => winAction -= function;

		[SerializeField] private LevelAchievementSetting levelAchievement;
		[SerializeField] private CongratulationUI congratulationUI;
		[SerializeField] private ScoreUI scoreUI;

		private int curScore;
		private ITimeService timeService;

		private int curAchievementIndex = -1;

		[Inject]
		private void Construct(ITimeService timeService)
		{
			this.timeService = timeService;
			
			timeService.TickingSubscribe(Tick);
			if (levelAchievement.achievements != null && levelAchievement.achievements.Count > 0)
				curAchievementIndex = 0;
		}

		public void OnDestroy()
		{
			timeService.TickingUnsubscribe(Tick);
		}

		private void Tick(int time)
		{
			scoreUI.ShowScore(time);

			if (curAchievementIndex == -1)
				return;

			if (time == levelAchievement.achievements[curAchievementIndex].needScore)
			{
				ShowCongratulation();

				if (levelAchievement.achievements.Count - 1 > curAchievementIndex)
					curAchievementIndex++;
				else
					curAchievementIndex = -1;
			}
			
			if (time >= levelAchievement.winResult)
				winAction?.Invoke(levelAchievement.nextLevelName);
		}

		private void ShowCongratulation()
		{
			congratulationUI.ShowPanel(levelAchievement.achievements[curAchievementIndex].CongratulationsText);
		}
	}
}
