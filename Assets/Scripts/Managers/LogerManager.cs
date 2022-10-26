using UnityEngine;
using Zenject;

namespace Managers
{
	public class LogerManager : MonoBehaviour
	{
		[Inject]
		private void Construct()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
