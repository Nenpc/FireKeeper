using GameView;
using GameLogic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
	public enum LogSize
	{
		Small,
		Medium,
		Large
	}

	public class LogManager : BaseGameManager
	{
		[SerializeField] private LogSettings logSettings;
		[SerializeField] private Transform pullContainer;
		[SerializeField] private Transform gameContainer;
		[SerializeField] private Terrain terrain;

		private List<Log> unusedLogs;
		private List<Log> usedLogs;
		private Vector2 terrainSize;

		private const int logCreatePositionY = 50;
		private const int borderIndentCreatePosition = 3;

		public override void Dispose()
		{
			TimeManager.Instance.Tiking -= TryCreateLog;

			usedLogs.Clear();
			usedLogs = null;

			unusedLogs.Clear();
			unusedLogs = null;
		}

		public override bool Initialize()
		{
			try
			{
				TimeManager.Instance.Tiking += TryCreateLog;

				usedLogs = new List<Log>();
				unusedLogs = new List<Log>();

				terrainSize = new Vector2(terrain.terrainData.size.x, terrain.terrainData.size.z);

				CreateStartLogs();

				return true;
			}
			catch
			{
				Debug.LogWarning("LogManager Launch problems");
				return false;
			}
		}

		public override string ManagerName()
		{
			return "Log";
		}

		private void TryCreateLog(int time = 0)
		{
			if (usedLogs.Count >= logSettings.maxLogCount)
				return;

			var curLog = logSettings.GetLog((LogSize)Random.Range(0, logSettings.logs.Count));

			var logLogic = new Log(curLog.type, curLog.slowdown, curLog.capacity);
			var logView = Instantiate(
				curLog.prefab,
				FindPosition(),
				Quaternion.Euler(0, Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));
			logView.transform.SetParent(gameContainer);
			logView.Initialize(logLogic, FindPosition());
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

		private void ReturnToPull(Log log)
		{
			log.view.Hide();
			log.transform.SetParent(pullContainer);
			log.transform.position = Vector3.zero;
			log.LogBurned -= ReturnToPull;

			usedLogs.Remove(log);
			unusedLogs.Add(log);
		}

		public void CreateLog(LogView log)
		{
			var position = FindPosition();
		}

		public Vector3 FindPosition()
		{
			bool haveValue = false;
			int maxTryAmount = 5;
			while (!haveValue  && maxTryAmount > 0)
			{
				var createPosition = new Vector3(
					Random.Range(borderIndentCreatePosition, terrainSize.x - borderIndentCreatePosition),
					logCreatePositionY,
					Random.Range(borderIndentCreatePosition, terrainSize.y - borderIndentCreatePosition));

				RaycastHit hit;
				if (Physics.Raycast(createPosition, Vector3.down, out hit, 100))
				{
					return hit.point + Vector3.up;
				}
				maxTryAmount--;

				if (maxTryAmount == 0)
				{
					Debug.LogWarning("Can't find log position");
				}
			}

			return Vector3.zero;
		}
	}
}
