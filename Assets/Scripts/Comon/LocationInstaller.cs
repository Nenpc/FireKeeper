using GameLogic;
using Managers;
using UnityEngine;
using Zenject;

namespace GameView.Comon
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LogManager logManager;
        [SerializeField] private ImprovementManager improvementManager;
        [SerializeField] private Managers.GameLogic gameLogic;
        
        [Space]
        [SerializeField] private TimeManager timeManagerPrefab;
        [SerializeField] private ProgressManager progressManagerPrefab;
        [SerializeField] private InputManager inputManagerPrefab;
        
        [Space]
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform playerStartPosition;
        [SerializeField] private BonfireView bonfireViewPrefab;
        [SerializeField] private Transform bonfireStartPosition;
        
        public override void InstallBindings()
        {
            InstantiateWithBind();
            OnlyBind();
        }

        private void InstantiateWithBind()
        {
            TimeManager timeManager = Container.InstantiatePrefabForComponent<TimeManager>(timeManagerPrefab);
            Container
                .Bind<TimeManager>()
                .FromInstance(timeManager)
                .AsSingle()
                .NonLazy();
            
            ProgressManager progressManager = Container.InstantiatePrefabForComponent<ProgressManager>(progressManagerPrefab);
            Container
                .Bind<ProgressManager>()
                .FromInstance(progressManager)
                .AsSingle()
                .NonLazy();

            InputManager inputManager = Container.InstantiatePrefabForComponent<InputManager>(inputManagerPrefab);
            Container
                .Bind<InputManager>()
                .FromInstance(inputManager)
                .AsSingle()
                .NonLazy();

            Player player = Container.InstantiatePrefabForComponent<Player>(
                playerPrefab, 
                playerStartPosition.position,
                Quaternion.identity, 
                null);
            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle()
                .NonLazy();

            Bonfire bonfire = new Bonfire(1, 100, timeManager);
            Container
                .Bind<Bonfire>()
                .FromInstance(bonfire)
                .AsSingle()
                .NonLazy();
            
            BonfireView bonfireView = Container.InstantiatePrefabForComponent<BonfireView>(
                bonfireViewPrefab, 
                bonfireStartPosition.position,
                Quaternion.identity, 
                null);
            Container
                .Bind< BonfireView>()
                .FromInstance(bonfireView)
                .AsSingle()
                .NonLazy();
        }

        private void OnlyBind()
        {
            Container
                .Bind<LogManager>()
                .FromInstance(logManager)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ImprovementManager>()
                .FromInstance(improvementManager)
                .AsSingle()
                .NonLazy();
                        
            Container
                .Bind<Managers.GameLogic>()
                .FromInstance(gameLogic)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<UIManager>()
                .FromInstance(uiManager)
                .AsSingle()
                .NonLazy();
        }
    }
}