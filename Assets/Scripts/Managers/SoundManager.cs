using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace Managers
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private SoundSettings soundSettings;
		[SerializeField] private SceneManagerSetting sceneManagerSetting;
		[SerializeField] private AudioSource audioSource;

		private SceneName currentScene = SceneName.Menu;
		private AudioClip currentAudioClip;

		[Inject]
		private void Construct()
		{
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
			StartNextSound();
			
			DontDestroyOnLoad(gameObject);
		}

		public void OnDestroy()
		{
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
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

