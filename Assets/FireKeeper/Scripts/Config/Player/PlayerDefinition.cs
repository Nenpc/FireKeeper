using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FireKeeper.Config
{
    [System.Serializable]
    public sealed class PlayerDefinition : IPlayerDefinition
    {
        [SerializeField] private float _stepSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _stamina;
        [SerializeField] private AssetReferenceGameObject _playerPrefab;

        public float StepSpeed => _stepSpeed;
        public float RunSpeed => _runSpeed;
        public float Stamina => _stamina;
        public AssetReferenceGameObject PlayerPrefab => _playerPrefab;
    }
}