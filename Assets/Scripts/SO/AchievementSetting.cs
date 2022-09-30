using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
	public int needScore;
	public string CongratulationsText;
}

[CreateAssetMenu(fileName = "AchievementSetting", menuName = "GameSettings/AchievementSetting", order = 5)]
public class AchievementSetting : ScriptableObject
{
	public List<Achievement> achievements;
}
