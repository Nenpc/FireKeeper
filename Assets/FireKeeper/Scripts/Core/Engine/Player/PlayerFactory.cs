using System.ComponentModel;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;
using Zenject;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerFactory : IPlayerFactory
    {
        private readonly IPlayerConfig _playerConfig;
        private readonly IPlayerController _playerController;
        
        private PlayerView _playerView;

        public PlayerFactory(IPlayerConfig playerConfig, IPlayerController playerController)
        {
            _playerConfig = playerConfig;
            _playerController = playerController;
        }

        public async UniTask<PlayerView> CreatePlayerAsync(Vector3 position)
        {
            if (_playerView == null)
            {
                var definition = _playerConfig.GetDefinition();
                return await CreatePlayerAsync(definition, position);
            }
            else
            {
                _playerView.gameObject.SetActive(true);
                return _playerView;
            }
        }
        
        private async UniTask<PlayerView> CreatePlayerAsync(IPlayerDefinition playerDefinition, Vector3 position)
        {
            var playerGo = await playerDefinition.PlayerPrefab.InstantiateAsync(position, Quaternion.identity);
            var playerView = playerGo.GetComponent<PlayerView>();

            _playerController.UpdateView(playerView);
            
            playerView.Initialize(_playerController);
            
            return playerView;
        }
        
        public void Hide()
        {
            _playerView.gameObject.SetActive(false);
        }
    }
}