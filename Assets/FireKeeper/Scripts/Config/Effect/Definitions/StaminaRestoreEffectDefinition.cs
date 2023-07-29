using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Effect/" + nameof(StaminaRestoreEffectDefinition),
        fileName = nameof(StaminaRestoreEffectDefinition))]
    public sealed class StaminaRestoreEffectDefinition : EffectDefinitionAbstract
    {
        [SerializeField] private string _id;
        [SerializeField] private int _power;

        public string Id => _id;
        public int Power => _power;

        public override IEffect GetEffect()
        {
            return new StaminaRestoreEffect(this);
        }
    }
}