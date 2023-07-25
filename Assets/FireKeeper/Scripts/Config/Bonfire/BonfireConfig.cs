using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/" + nameof(BonfireConfig), fileName = nameof(BonfireConfig))]
    public sealed class BonfireConfig : ScriptableObject, IBonfireConfig
    {
        [SerializeField] private BonfireDefinition _definition;

        public IBonfireDefinition GetDefinition() =>  _definition;
    }
}