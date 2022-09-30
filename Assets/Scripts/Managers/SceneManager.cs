using UnityEngine;

namespace Managers
{
	public class SceneManager : BaseGameManager
	{
		[SerializeField] private SceneManagerSetting sceneManagerSetting;

		private void Awake()
		{
			Initialize();
		}

		public override void Dispose()
		{
		}

		public override bool Initialize()
		{
			if (sceneManagerSetting == null)
			{
				Debug.LogError("SceneManager not initialized");
				return false;
			}
			return true;
		}

		public void LoadScene(SceneName sceneName)
		{
			foreach (var scene in sceneManagerSetting.scenes)
			{
				if (sceneName == scene.sceneName)
				{
					UnityEditor.SceneAsset sceneObject = (UnityEditor.SceneAsset)scene.scene;
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
					UnityEditor.SceneAsset sceneObject = (UnityEditor.SceneAsset)scene.scene;
					UnityEngine.SceneManagement.SceneManager.LoadScene(scene.scene.name);
					break;
				}
			}
		}

		public override string ManagerName()
		{
			throw new System.NotImplementedException();
		}
	}
}
