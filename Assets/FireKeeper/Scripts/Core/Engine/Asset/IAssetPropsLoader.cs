using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public interface IAssetPropsLoader
    {
        UniTask<GameObject> LoadPropsAsync(string key, Transform parent, Vector3 position);
        
        UniTask<Sprite> LoadSpriteAsync(string key);
    }
}