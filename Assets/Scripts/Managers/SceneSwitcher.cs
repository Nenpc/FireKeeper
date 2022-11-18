using UnityEngine;
using Zenject;

namespace Managers
{
	public class SceneSwitcher : MonoBehaviour
	{
		[SerializeField] private SceneManagerSetting sceneManagerSetting;

		[Inject]
		private void Construct()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void LoadScene(SceneName sceneName)
		{
			foreach (var scene in sceneManagerSetting.scenes)
			{
				if (sceneName == scene.sceneName)
				{
					//UnityEditor.SceneAsset sceneObject = (UnityEditor.SceneAsset)scene.scene;
					UnityEngine.SceneManagement.SceneManager.LoadScene(scene.sceneName.ToString());
					break;
				}
			}
		}

		public void LoadScene(SceneName sceneName, string param)
		{
			foreach (var scene in sceneManagerSetting.scenes)
			{
				if (sceneName == scene.sceneName)
				{
					//UnityEditor.SceneAsset sceneObject = (UnityEditor.SceneAsset)scene.scene;
					UnityEngine.SceneManagement.SceneManager.LoadScene(scene.scene.name);
					break;
				}
			}
		}

		public string ManagerName()
		{
			return "Scene switcher";
		}
	}
}
