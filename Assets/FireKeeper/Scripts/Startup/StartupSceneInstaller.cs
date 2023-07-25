using UnityEngine;
using Zenject;

namespace Core.Startup
{
    public sealed class StartupSceneInstaller : MonoInstaller
    {
        [SerializeField] private string _coreSceneName;
        [SerializeField] private LoadingScreenView _loadingScreenView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StartupSceneInitializer>().AsSingle();
            
            Container.BindInterfacesTo<SceneSwitcher>().AsSingle().WithArguments(_coreSceneName);
            Container.BindInterfacesTo<StartupCoreExtraInstaller>().AsSingle();
            
            Container.BindInstance(_loadingScreenView).AsSingle();
        }
    }
}