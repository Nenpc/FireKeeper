using AdditionalFunctions;
using Managers;
using UnityEngine;
using Zenject;

namespace GameView.Comon
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneManager sceneManagerPrefab;
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
            
            SceneManager sceneManager = Container.InstantiatePrefabForComponent<SceneManager>(sceneManagerPrefab);
            Container
                .Bind<SceneManager>()
                .FromInstance(sceneManager)
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