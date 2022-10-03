using GameLogic;
using System.Collections.Generic;
using GameUI;
using UnityEngine;

[System.Serializable]
public class ImprovementSetting
{
	public ImprovementType type;
	[Range(0, 100f)]
	public float time;
	[Range(0f, 100)]
	public float capacity;

	public GameView.ImprovementView prefab;

	public ImprovementIcon Icon;
}

[CreateAssetMenu(fileName = "ImprovementSettings", menuName = "GameSettings/ImprovementSettings", order = 8)]
public class ImprovementSettings : ScriptableObject
{
	[Range(0f, 50)]
	public int startImprovementCount;
	[Range(0f, 50)]
	public int maxImprovementCount;

	public List<ImprovementSetting> improvements;

	public ImprovementSetting GetImprovement(ImprovementType type)
	{
		foreach (var improvement in improvements)
		{
			if (improvement.type == type)
				return improvement;
		}

		Debug.LogError("A non-existent improvement was requested. Check ImprovementSetting!");
		return null;
	}
}
