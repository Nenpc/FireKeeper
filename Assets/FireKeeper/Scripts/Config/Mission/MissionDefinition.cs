using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class MissionDefinition : IMissionDefinition
    {
        [SerializeField] public string _id;
        [SerializeField] public AssetReferenceGameObject _mapView;
        [Range(0,100)]
        [SerializeField] public int _startFuel;
        [Range(0,100)]
        [SerializeField] public int _startBonus;
        [Range(0,100)]
        [SerializeField] public int _startEnemy;
        [Range(0,100)]
        [SerializeField] public int _maxFuel;
        [Range(0,100)]
        [SerializeField] public int _maxBonus;
        [Range(0,100)]
        [SerializeField] public int _maxEnemy;
        [Range(0,100)]
        [SerializeField] public int _maxBonfire;
        [Range(0,10000)]
        [SerializeField] public int _winResult;
        [SerializeField] public Vector2 _size;
        [Range(0,10)]
        [SerializeField] public int _indent;

        public string Id => _id;
        public AssetReferenceGameObject MapView => _mapView;
        public int StartFuel => _startFuel;
        public int StartBonus => _startBonus;
        public int StartEnemy => _startEnemy;
        public int MaxFuel => _maxFuel;
        public int MaxBonus => _maxBonus;
        public int MaxEnemy => _maxEnemy;
        public int MaxBonfire => _maxBonfire;
        public int WinTime => _winResult;
        public Vector2 Size => _size;
        public int Indent => _indent;
    }
}