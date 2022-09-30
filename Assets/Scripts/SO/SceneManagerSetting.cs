using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
	Menu,
	SummerLevel,
	SpringLevel,
	AutumnLevel,
	WinterLevel
}

[System.Serializable]
public class SceneSetting
{
	public SceneName sceneName;
	public Object scene;
}

[CreateAssetMenu(fileName = "SceneManagerSetting", menuName = "GameSettings/SceneManagerSetting", order = 6)]
public class SceneManagerSetting : ScriptableObject
{
	public List<SceneSetting> scenes;

	private void OnValidate()
	{
		for (int i = 0; i < scenes.Count; i++)
		{
			if (scenes[i].scene.GetType().ToString() != "UnityEditor.SceneAsset")
				Debug.LogError($"The scene named {scenes[i].sceneName.ToString()} has the wrong scene type set.");
		}
	}
}
