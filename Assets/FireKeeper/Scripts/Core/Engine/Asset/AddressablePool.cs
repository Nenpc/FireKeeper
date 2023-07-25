using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Core.Engine
{
    public class AddressablePool<T> where T : MonoBehaviour
    {
        private readonly string _addressableKey;
        private readonly Transform _parent;

        private readonly Stack<T> _stackSpawnedViews = new Stack<T>();

        public AddressablePool(string addressableKey, Transform parent = null)
        {
            _addressableKey = addressableKey;
            _parent = parent;
        }

        private async UniTask<T> AllocateViewAsync(AssetReferenceGameObject asset,Vector3 position)
        {
            var gameObject = await LoadAsync(asset, position);
            return gameObject.GetComponent<T>();
        }

        private async UniTask<GameObject> LoadAsync(AssetReferenceGameObject asset, Vector3 position)
        {
            var gameObject = await asset.InstantiateAsync(position, Quaternion.identity, _parent);
            if (gameObject == default)
            {
                Debug.LogError($"Cannot find asset for key {_addressableKey}");
                return default;
            }

            return gameObject;
        }

        public async UniTask<T> Get(AssetReferenceGameObject asset, Vector3 position)
        {
            if (_stackSpawnedViews.Count <= 0)
            {
                return await AllocateViewAsync(asset, position);
            }
            
            var view = _stackSpawnedViews.Pop();
            view.gameObject.SetActive(true);
            view.transform.position = position;
            return view;
        }
        
        public void Return(T view)
        {
            view.gameObject.SetActive(false);
            view.transform.position = Vector3.zero;
            _stackSpawnedViews.Push(view);
        }
    }
}