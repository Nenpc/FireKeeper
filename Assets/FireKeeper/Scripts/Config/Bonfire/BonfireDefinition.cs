using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class BonfireDefinition : IBonfireDefinition
    {
        [SerializeField] public int _life;
        [SerializeField] public AssetReferenceGameObject _bonefirePrefab;
        
        public int Life => _life;
        public AssetReferenceGameObject BonfirePrefab => _bonefirePrefab;
    }
}