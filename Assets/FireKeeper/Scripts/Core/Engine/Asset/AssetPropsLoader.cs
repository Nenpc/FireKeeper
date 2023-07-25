using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Core.Engine
{
    public class AssetPropsLoader : IAssetPropsLoader
    {
        public async UniTask<GameObject> LoadPropsAsync(string key, Transform parent, Vector3 position)
        {
            var gameObject = await Addressables.LoadAssetAsync<GameObject>(key);

            if (gameObject == default)
            {
                Debug.LogError($"Cannot find asset for key {key}");
                return null;
            }

            var props = Object.Instantiate(gameObject, position, Quaternion.identity, parent);
            return props;
        }
        
        public async UniTask<Sprite> LoadSpriteAsync(string key)
        {
            return await Addressables.LoadAssetAsync<Sprite>(key);
        }
    }
}