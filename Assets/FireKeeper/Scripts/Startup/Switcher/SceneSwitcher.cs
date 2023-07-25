using System;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using FireKeeper.Core.Engine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Startup
{
    public sealed class SceneSwitcher : ISceneSwitcher
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;
        private readonly LoadingScreenView _loadingScreenView;
        private readonly string _coreSceneName;

        public SceneSwitcher(ZenjectSceneLoader zenjectSceneLoader,
            LoadingScreenView loadingScreenView,
            string coreSceneName)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
            _loadingScreenView = loadingScreenView;
            _coreSceneName = coreSceneName;
        }

        public async UniTask SwitchToCoreSceneAsync(IMissionDefinition missionDefinition)
        {
            await LoadSceneAsync(_coreSceneName, diContainer =>
            {
                diContainer.Install<CoreExtraInstaller>(new object[] { missionDefinition});
                diContainer.Install<StartupCoreExtraInstaller>();
            });

            var coreScene = SceneManager.GetSceneByName(_coreSceneName);
            SceneManager.SetActiveScene(coreScene);
            _loadingScreenView.HideAsync();
        }

        public async UniTask SwitchToStartupSceneAsync()
        {
            await UnloadSceneAsync(_coreSceneName);
        }

        private async UniTask LoadSceneAsync(string sceneName, Action<DiContainer> extraBindingCallback = null)
        {
            await _zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive, extraBindingCallback);
        }

        private async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}