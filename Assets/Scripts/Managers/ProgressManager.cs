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
		event Action<SceneName> WinEvent;
	}
	
	public class ProgressManager : MonoBehaviour, IProgressManager, IWin
	{
		public event Action<SceneName> WinEvent;

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
			
			timeService.TickingEvent += Tick;
			if (levelAchievement.achievements != null && levelAchievement.achievements.Count > 0)
				curAchievementIndex = 0;
		}

		public void OnDestroy()
		{
			timeService.TickingEvent -= Tick;
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
				WinEvent?.Invoke(levelAchievement.nextLevelName);
		}

		private void ShowCongratulation()
		{
			congratulationUI.ShowPanel(levelAchievement.achievements[curAchievementIndex].CongratulationsText);
		}
	}
}
