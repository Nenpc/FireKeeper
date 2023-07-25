using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Effect/" + nameof(SlowEffectDefinition), fileName = nameof(SlowEffectDefinition))]
    public sealed class SlowEffectDefinition : EffectDefinitionAbstract
    {
        [SerializeField] private string _id;
        [SerializeField] private float _power;

        public string Id => _id;
        public float Power => _power;
        
        public override IEffect GetEffect()
        {
            return new SlowPlayerEffect(this);
        }
    }
}