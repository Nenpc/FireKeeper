using GameUI;
using UnityEngine;

namespace Managers
{
	public class ProgressManager : BaseGameManager
	{
		[SerializeField] private AchievementSetting achievement;
		[SerializeField] private CongratulationUI congratulationUI;
		[SerializeField] private ScoreUI scoreUI;

		private int curScore;

		// если (curAchievementIndex == -1) значит либо нет ачивок в списке либо мы все показали
		private int curAchievementIndex = -1;

		public override void Dispose()
		{
			TimeManager.Instance.Tiking -= Tick;
		}

		public override bool Initialize()
		{
			if (achievement == null)
				return false;

			if (congratulationUI == null)
				return false;

			TimeManager.Instance.Tiking += Tick;
			if (achievement.achievements != null && achievement.achievements.Count > 0)
				curAchievementIndex = 0;

			return true;
		}

		public void Tick(int time)
		{
			scoreUI.ShowScore(time);

			if (curAchievementIndex == -1)
				return;

			if (time == achievement.achievements[curAchievementIndex].needScore)
			{
				ShowCongratulation();

				if (achievement.achievements.Count - 1 > curAchievementIndex)
					curAchievementIndex++;
				else
					curAchievementIndex = -1;
			}
		}

		private void ShowCongratulation()
		{
			congratulationUI.ShowPanel(achievement.achievements[curAchievementIndex].CongratulationsText);
		}

		public override string ManagerName()
		{
			return "Progress";
		}
	}
}
