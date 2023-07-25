using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class MapView : MonoBehaviour
    {
        [SerializeField] private Transform _bonfire;
        [SerializeField] private Transform _player;
        
        public Vector3 GetBonfirePosition()
        {
            return _bonfire.position;
        }

        public Vector3 GetPlayerPosition()
        {
            return _player.position;
        }
    }
}