using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
	public class InitializeManager : MonoBehaviour
	{
		[SerializeField] private List<BaseGameManager> gameManagers;

		private void Awake()
		{
			foreach (var manager in gameManagers)
			{
				if (!manager.Initialize())
				{
					Debug.LogError("Failed to start manager " + manager.ManagerName());
				}
			}
		}
	}
}
