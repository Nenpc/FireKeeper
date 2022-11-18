using GameLogic;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Managers
{
	public interface ILogMaker
	{
		
	}

	public enum LogSize
	{
		Small,
		Medium,
		Large
	}

	public sealed class LogMaker : BaseTakingObjects, ILogMaker
	{
		[Header("Personal setting")]
		[SerializeField] private LogSettings logSettings;

		private List<Log> unusedLogs;
		private List<Log> usedLogs;

		[Inject]
		private void Construct(ITimeService timeService, IBonfire bonfire)
		{
			this.timeService = timeService;
			this.bonfire = bonfire;

			timeService.TickingEvent += TryCreateLog;

			usedLogs = new List<Log>();
			unusedLogs = new List<Log>();

			terrainSize = new Vector2(terrain.terrainData.size.x, terrain.terrainData.size.z);

			CreateStartLogs();
		}

		public void OnDestroy()
		{
			timeService.TickingEvent -= TryCreateLog;

			usedLogs.Clear();
			usedLogs = null;

			unusedLogs.Clear();
			unusedLogs = null;
		}

		private void TryCreateLog(int time = 0)
		{
			if (usedLogs.Count >= logSettings.maxLogCount)
				return;

			var curLog = logSettings.GetLog((LogSize)Random.Range(0, logSettings.logs.Count));

			var logLogic = new global::GameLogic.Log(curLog.type, curLog.slowdown, curLog.capacity);
			var logView = Instantiate(
				curLog.prefab,
				FindPosition(),
				Quaternion.Euler(0, Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
			logView.transform.SetParent(gameContainer);
			logView.Construct(logLogic, FindPosition());
			logLogic.LogBurned += ReturnToPull;

			usedLogs.Add(logLogic);
		}

		private void CreateStartLogs()
		{
			for (int i = 0; i <= logSettings.startLogCount; i++)
			{
				TryCreateLog();
			}		
		}

		private void ReturnToPull(global::GameLogic.Log log)
		{
			log.view.Hide();
			log.transform.SetParent(pullContainer);
			log.transform.position = Vector3.zero;
			log.LogBurned -= ReturnToPull;

			usedLogs.Remove(log);
			unusedLogs.Add(log);
		}
	}
}
