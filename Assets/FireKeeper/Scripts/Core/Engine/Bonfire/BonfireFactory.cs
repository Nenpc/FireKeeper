using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class BonfireFactory : IBonfireFactory
    {
        private readonly IBonfireConfig _bonfireConfig;
        
        private BonfireView _bonfireView;

        public BonfireFactory(IBonfireConfig bonfireConfig)
        {
            _bonfireConfig = bonfireConfig;
        }

        public async UniTask<BonfireView> CreateBonfireAsync(Vector3 position)
        {
            if (_bonfireView == null)
            {
                var definition = _bonfireConfig.GetDefinition();
                return await CreateBonfireAsync(definition, position);
            }
            else
            {
                _bonfireView.gameObject.SetActive(true);
                return _bonfireView;
            }
        }
        
        private async UniTask<BonfireView> CreateBonfireAsync(IBonfireDefinition bonfireDefinition, Vector3 position)
        {
            var bonfireGo = await bonfireDefinition.BonfirePrefab.InstantiateAsync(position, Quaternion.identity);
            var bonfireView = bonfireGo.GetComponent<BonfireView>();
            bonfireView.Initialize(bonfireDefinition);
            
            return bonfireView;
        }
        
        public void Hide()
        {
            _bonfireView.gameObject.SetActive(false);
        }
    }
}