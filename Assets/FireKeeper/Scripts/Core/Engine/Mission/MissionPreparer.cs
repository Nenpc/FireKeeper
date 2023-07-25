using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class MissionPreparer : IMissionPreparer
    {
        private IPlayerFactory _playerFactory;
        private IBonfireFactory _bonfireFactory;
        private IMissionConfig _missionConfig;
        private IBonusFactory _bonusFactory;
        private IFuelFactory _fuelFactory;
        private IEnemyFactory _enemyFactory;
        
        public MissionPreparer(IMissionConfig missionConfig,
            IPlayerFactory playerFactory,
            IBonfireFactory bonfireFactory,
            IBonusFactory bonusFactory,
            IFuelFactory fuelFactory,
            IEnemyFactory enemyFactory)
        {
            _missionConfig = missionConfig;
            _playerFactory = playerFactory;
            _bonfireFactory = bonfireFactory;
            _bonusFactory = bonusFactory;
            _fuelFactory = fuelFactory;
            _enemyFactory = enemyFactory;
        }

        public async UniTask PrepareMission(string id)
        {
            var missionDefinition = _missionConfig.GetDefinition(id);
            
            var mapGo = await missionDefinition.MapView.InstantiateAsync(Vector3.zero, Quaternion.identity);
            var mapView = mapGo.GetComponent<MapView>();

            if (mapView == null)
            {
                Debug.LogError($"No map view for mission {missionDefinition.Id}");
                return;
            }

            await _playerFactory.CreatePlayerAsync(mapView.GetPlayerPosition());

            await _bonfireFactory.CreateBonfireAsync(mapView.GetBonfirePosition());
            
            await CreateBonuses(missionDefinition);
            await CreateFuels(missionDefinition);
            await CreateEnemies(missionDefinition);
        }

        private async UniTask CreateBonuses(IMissionDefinition missionDefinition)
        {
            for (int i = 0; i < missionDefinition.StartBonus; i++)
            {
                var position = _missionConfig.GetRandomPosition(missionDefinition.Id);
                await _bonusFactory.CreateRandomBonusAsync(position);
            }
        }
        
        private async UniTask CreateFuels(IMissionDefinition missionDefinition)
        {
            for (int i = 0; i < missionDefinition.StartFuel; i++)
            {
                var position = _missionConfig.GetRandomPosition(missionDefinition.Id);
                await _fuelFactory.CreateRandomFuelAsync(position);
            }
        }
        
        private async UniTask CreateEnemies(IMissionDefinition missionDefinition)
        {
            for (int i = 0; i < missionDefinition.StartEnemy; i++)
            {
                var position = _missionConfig.GetRandomPosition(missionDefinition.Id);
                await _enemyFactory.CreateRandomEnemyAsync(position);
            }
        }
    }
}