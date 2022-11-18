using GameLogic;
using GameView;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Managers
{
	public class ImprovementMaker : BaseTakingObjects
	{
		[Header("Personal setting")]
		[SerializeField] private ImprovementSettings improvementSettings;
		
		private List<Improvement> unusedImprovements;
		private List<Improvement> usedImprovements;

		[Inject]
		private void Construct(ITimeService timeService, IBonfire bonfire)
		{
			this.timeService = timeService;
			this.bonfire = bonfire;

			this.timeService.TickingEvent += TryCreateImprovement;

			usedImprovements = new List<Improvement>();
			unusedImprovements = new List<Improvement>();

			terrainSize = new Vector2(terrain.terrainData.size.x, terrain.terrainData.size.z);

			CreateStartLogs();
		}

		public void OnDestroy()
		{
			timeService.TickingEvent -= TryCreateImprovement;
			
			unusedImprovements.Clear();
			usedImprovements.Clear();
		}

		private void TryCreateImprovement(int time = 0)
		{
			if (usedImprovements.Count >= improvementSettings.maxImprovementCount)
				return;

			var curImprovement = improvementSettings.GetImprovement((ImprovementType)Random.Range(0, improvementSettings.improvements.Count));

			var improvementLogic = new global::GameLogic.Improvement(curImprovement.type, curImprovement.time, curImprovement.capacity);
			var improvementView = Instantiate(
				curImprovement.prefab,
				FindPosition(),
				Quaternion.Euler(-90,0,0)).GetComponent<ImprovementView>();
			improvementView.transform.SetParent(gameContainer);
			improvementView.Construct(improvementLogic, FindPosition());
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

		private void ReturnToPull(global::GameLogic.Improvement improvement)
		{
			improvement.View.Hide();
			improvement.View.transform.SetParent(pullContainer);
			improvement.View.transform.position = Vector3.zero;
			improvement.ImprovementUsed-= ReturnToPull;

			usedImprovements.Remove(improvement);
			unusedImprovements.Add(improvement);
		}
	}
}
