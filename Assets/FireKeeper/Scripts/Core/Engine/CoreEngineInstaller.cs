using FireKeeper.Config;
using UnityEngine;
using Zenject;

namespace  FireKeeper.Core.Engine
{
    public sealed class CoreEngineInstaller : MonoInstaller
    {
        private int _systemOrder;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CoreEngineInitializer>().AsSingle();
            
            BindMain();
        }

        private void BindMain()
        {
            Container.BindInterfacesTo<AssetPropsLoader>().AsSingle();
            Container.BindInterfacesTo<ProgressController>().AsSingle();
            Container.BindInterfacesTo<PlayerController>().AsSingle();
            Container.BindInterfacesTo<BonfireController>().AsSingle();
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            Container.BindInterfacesTo<BonfireFactory>().AsSingle();
            Container.BindInterfacesTo<CoreTimeController>().AsSingle();
            
            var fuelPoolParent = Container.CreateEmptyGameObject("FuelViewPool").transform;
            Container.BindInterfacesTo<FuelFactory>().AsSingle().WithArguments(fuelPoolParent);
            
            var enemyPoolParent = Container.CreateEmptyGameObject("EnemyViewPool").transform;
            Container.BindInterfacesTo<EnemyFactory>().AsSingle().WithArguments(enemyPoolParent);

            var bonusPoolParent = Container.CreateEmptyGameObject("BonusViewPool").transform;
            Container.BindInterfacesTo<BonusFactory>().AsSingle().WithArguments(bonusPoolParent);
            
            Container.BindInterfacesTo<MissionPreparer>().AsSingle();
        }
    }
}