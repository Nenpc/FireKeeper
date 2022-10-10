using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Managers
{
	public class SoundManager : BaseGameManager
	{
		[SerializeField] private SoundSettings soundSettings;
		[SerializeField] private SceneManagerSetting sceneManagerSetting;
		[SerializeField] private AudioSource audioSource;

		private SceneName currentScene = SceneName.Menu;
		private AudioClip currentAudioClip;

		private void Awake()
		{
			Initialize();
		}

		public override void Dispose()
		{
			throw new System.NotImplementedException();
		}

		public override bool Initialize()
		{
			if (soundSettings == null)
				return false;
			
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			StartNextSound();
			
			DontDestroyOnLoad(gameObject);
			return true;
		}

		public override string ManagerName()
		{
			return "Sound";
		}
		
		void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
			foreach (var sceneSetting in sceneManagerSetting.scenes)
			{
				if (sceneSetting.scene.name == scene.name)
				{
					currentScene = sceneSetting.sceneName;
					StartNextSound();
				}
			}
		}

		public void UpdateBackgroundSoundValue(float value)
		{
			audioSource.volume = value;
		}

		private void StartNextSound()
		{
			audioSource.Stop();
			foreach (var levelSound in soundSettings.levelSounds)
			{
				if (levelSound.sceneName == currentScene)
				{
					currentAudioClip = levelSound.backgroundMusic[Random.Range(0,levelSound.backgroundMusic.Count)];
					if (currentAudioClip != null)
					{
						audioSource.clip = currentAudioClip;
						audioSource.Play();
					}
				}
			}
		}
	}
}

