using AdditionalFunctions;
using Managers;
using UnityEngine;
using Zenject;

namespace GameView.Comon
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneSwitcher sceneSwitcherPrefab;
        [SerializeField] private SoundManager soundManagerPrefab;
        [SerializeField] private LogerManager logerManagerPrefab;
        [SerializeField] private AlertSystem alertSystemPrefab;
        
        public override void InstallBindings()
        {
            DontDestroyOnLoad(gameObject);
            
            SoundManager soundManager = Container.InstantiatePrefabForComponent<SoundManager>(soundManagerPrefab);
            Container
                .Bind<SoundManager>()
                .FromInstance(soundManager)
                .AsSingle()
                .NonLazy();
            
            SceneSwitcher sceneSwitcher = Container.InstantiatePrefabForComponent<SceneSwitcher>(sceneSwitcherPrefab);
            Container
                .Bind<SceneSwitcher>()
                .FromInstance(sceneSwitcher)
                .AsSingle()
                .NonLazy();
            
            LogerManager logerManager = Container.InstantiatePrefabForComponent<LogerManager>(logerManagerPrefab);
            Container
                .Bind<LogerManager>()
                .FromInstance(logerManager)
                .AsSingle()
                .NonLazy();
            
            AlertSystem alertSystem = Container.InstantiatePrefabForComponent<AlertSystem>(alertSystemPrefab);
            Container
                .Bind<AlertSystem>()
                .FromInstance(alertSystem)
                .AsSingle()
                .NonLazy();
        }
    }
}