using Managers;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LogSetting
{
	public LogSize type;
	[Range(0f, 10)]
	public int maxCount;
	[Range(0, 0.99f)]
	public float slowdown;
	[Range(0f, 100)]
	public float capacity;

	public GameView.LogView prefab;
}

[CreateAssetMenu(fileName = "LogSettings", menuName = "GameSettings/LogSettings", order = 2)]
public class LogSettings : ScriptableObject
{
	[Range(0f, 50)]
	public int startLogCount;
	[Range(0f, 50)]
	public int maxLogCount;

	public List<LogSetting> logs;

	public LogSetting GetLog(LogSize type)
	{
		foreach(var log in logs)
		{
			if (log.type == type)
				return log;
		}

		Debug.LogError("A non-existent log was requested. Check LogSetting!");
		return null;
	}
}
