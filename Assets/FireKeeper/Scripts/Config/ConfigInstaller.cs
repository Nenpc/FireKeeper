using UnityEngine;
using Zenject;

namespace FireKeeper.Config
{
    public sealed class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private BonfireConfig _bonfireConfig;
        [SerializeField] private BonusConfig _bonusConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private FuelConfig _fuelConfig;
        [SerializeField] private MissionConfig _missionConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        public override void InstallBindings()
        {
            Container.BindInstance<IBonfireConfig>(_bonfireConfig).AsSingle();
            Container.BindInstance<IBonusConfig>(_bonusConfig).AsSingle();
            Container.BindInstance<IEnemyConfig>(_enemyConfig).AsSingle();
            Container.BindInstance<IFuelConfig>(_fuelConfig).AsSingle();
            Container.BindInstance<IMissionConfig>(_missionConfig).AsSingle();
            Container.BindInstance<IPlayerConfig>(_playerConfig).AsSingle();
        }
    }
}