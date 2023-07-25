using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(PlayerConfig), fileName = nameof(PlayerConfig))]
    public sealed class PlayerConfig : ScriptableObject, IPlayerConfig
    {
        [SerializeField] private PlayerDefinition _definition;

        public IPlayerDefinition GetDefinition() =>  _definition;
    }
}