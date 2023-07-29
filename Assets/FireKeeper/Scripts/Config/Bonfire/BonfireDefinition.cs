using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class BonfireDefinition : IBonfireDefinition
    {
        [SerializeField] public int _maxLife;
        [SerializeField] public float _fadingPerSecond;
        [SerializeField] public AssetReferenceGameObject _bonefirePrefab;
        
        public int MaxLife => _maxLife;
        public float FadingPerSecond => _fadingPerSecond;
        public AssetReferenceGameObject BonfirePrefab => _bonefirePrefab;
    }
}