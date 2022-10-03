using GameLogic;
using GameView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
	public class ImprovementManager : BaseGameManager
	{
		[SerializeField] private ImprovementSettings improvementSettings;
		[SerializeField] private Transform pullContainer;
		[SerializeField] private Transform gameContainer;
		[SerializeField] private Terrain terrain;

		private List<Improvement> unusedImprovements;
		private List<Improvement> usedImprovements;
		private Vector2 terrainSize;

		private const int improvementPositionY = 50;
		private const int borderIndentCreatePosition = 3;

		public override void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public override bool Initialize()
		{
			try
			{
				TimeManager.Instance.Tiking += TryCreateImprovement;

				usedImprovements = new List<Improvement>();
				unusedImprovements = new List<Improvement>();

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
			return "Improvement";
		}

		private void TryCreateImprovement(int time = 0)
		{
			if (usedImprovements.Count >= improvementSettings.maxImprovementCount)
				return;

			var curImprovement = improvementSettings.GetImprovement((ImprovementType)Random.Range(0, improvementSettings.improvements.Count));

			var improvementLogic = new Improvement(curImprovement.type, curImprovement.time, curImprovement.capacity);
			var improvementView = Instantiate(
				curImprovement.prefab,
				FindPosition(),
				Quaternion.Euler(-90,0,0)).GetComponent<ImprovementView>();
			improvementView.transform.SetParent(gameContainer);
			improvementView.Initialize(improvementLogic, FindPosition());
			improvementLogic.ImprovementUsed += ReturnToPull;

			usedImprovements.Add(improvementLogic);
		}

		private void CreateStartLogs()
		{
			for (int i = 0; i <= improvementSettings.startImprovementCount; i++)
			{
				TryCreateImprovement();
			}
		}

		private void ReturnToPull(Improvement improvement)
		{
			improvement.View.Hide();
			improvement.View.transform.SetParent(pullContainer);
			improvement.View.transform.position = Vector3.zero;
			improvement.ImprovementUsed-= ReturnToPull;

			usedImprovements.Remove(improvement);
			unusedImprovements.Add(improvement);
		}

		public void CreateImprovement(ImprovementView log)
		{
			var position = FindPosition();
		}

		public Vector3 FindPosition()
		{
			bool haveValue = false;
			int maxTryAmount = 5;
			while (!haveValue && maxTryAmount > 0)
			{
				var createPosition = new Vector3(
					Random.Range(borderIndentCreatePosition, terrainSize.x - borderIndentCreatePosition),
					improvementPositionY,
					Random.Range(borderIndentCreatePosition, terrainSize.y - borderIndentCreatePosition));

				RaycastHit hit;
				if (Physics.Raycast(createPosition, Vector3.down, out hit, 100))
				{
					return hit.point + (Vector3.up * 0.1f);
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
