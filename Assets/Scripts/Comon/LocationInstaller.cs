using GameLogic;
using Managers;
using UnityEngine;
using Zenject;

namespace GameView.Comon
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Managers.GameUI gameUI;
        [SerializeField] private LogMaker logMaker;
        [SerializeField] private ImprovementMaker improvementMaker;
        [SerializeField] private Managers.GameLogic gameLogic;
        [SerializeField] private TimeService timeService;
        [SerializeField] private ProgressManager progressManager;
        [SerializeField] private InputManager inputManager;
        
        [Space]
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform playerStartPosition;
        [SerializeField] private BonfireView bonfireViewPrefab;
        [SerializeField] private Transform bonfireStartPosition;
        
        public override void InstallBindings()
        {
            ManagerBind();
            CreateAndBindMainObject();
        }

        private void CreateAndBindMainObject()
        {
            BonfireView bonfireView = Instantiate(bonfireViewPrefab, bonfireStartPosition.position,Quaternion.identity);
            
            Bonfire bonfire = new Bonfire(1, 100, timeService, bonfireStartPosition.position, bonfireView);
            Container
                .Bind<IBonfire>()
                .FromInstance(bonfire)
                .AsSingle()
                .NonLazy();
            
            Player player = Container.InstantiatePrefabForComponent<Player>(
                playerPrefab, 
                playerStartPosition.position,
                Quaternion.identity, 
                null);
            Container
                .Bind<IPlayer>()
                .FromInstance(player)
                .AsSingle()
                .NonLazy();
        }

        private void ManagerBind()
        {
            Container
                .Bind<ITimeService>()
                .FromInstance(timeService)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IProgressManager>()
                .FromInstance(progressManager)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IWin>()
                .FromInstance(progressManager)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<InputManager>()
                .FromInstance(inputManager)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ILogMaker>()
                .FromInstance(logMaker)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ImprovementMaker>()
                .FromInstance(improvementMaker)
                .AsSingle()
                .NonLazy();
                        
            Container
                .Bind<Managers.GameLogic>()
                .FromInstance(gameLogic)
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IGameUI>()
                .FromInstance(gameUI)
                .AsSingle()
                .NonLazy();
        }
    }
}