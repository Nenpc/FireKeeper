using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
	public int needScore;
	public string CongratulationsText;
}

[CreateAssetMenu(fileName = "LevelAchievementSetting", menuName = "GameSettings/LevelAchievementSetting", order = 5)]
public class LevelAchievementSetting : ScriptableObject
{
	public List<Achievement> achievements;
	public int winResult;
	public SceneName nextLevelName;

	private void OnValidate()
	{
		foreach (var achievement in achievements)
		{
			if (winResult < achievement.needScore)
				Debug.LogError("Win result less then " + achievement.CongratulationsText);
		}
	}
}
