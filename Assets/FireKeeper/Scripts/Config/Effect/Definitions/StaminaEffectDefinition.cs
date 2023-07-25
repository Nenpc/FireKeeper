using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Effect/" + nameof(StaminaEffectDefinition), fileName = nameof(StaminaEffectDefinition))]
    public sealed class StaminaEffectDefinition : EffectDefinitionAbstract
    {
        [SerializeField] private string _id;
        [SerializeField] private int _power;

        public string Id => _id;
        public int Power => _power;

        public override IEffect GetEffect()
        {
            return new StaminaDamageEffect(this);
        }
    }
}