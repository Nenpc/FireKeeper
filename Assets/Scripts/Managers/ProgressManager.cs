using GameUI;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class ProgressManager : MonoBehaviour
	{
		[SerializeField] private AchievementSetting achievement;
		[SerializeField] private CongratulationUI congratulationUI;
		[SerializeField] private ScoreUI scoreUI;

		private int curScore;
		private TimeManager timeManager;

		// ���� (curAchievementIndex == -1) ������ ���� ��� ������ � ������ ���� �� ��� ��������
		private int curAchievementIndex = -1;

		[Inject]
		private void Construct(TimeManager timeManager)
		{
			this.timeManager = timeManager;
			
			timeManager.Tiking += Tick;
			if (achievement.achievements != null && achievement.achievements.Count > 0)
				curAchievementIndex = 0;
		}

		public void Ondestroy()
		{
			timeManager.Tiking -= Tick;
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
	}
}
