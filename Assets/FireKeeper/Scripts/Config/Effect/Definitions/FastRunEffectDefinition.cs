using UnityEngine;

namespace FireKeeper.Config
{
    [CreateAssetMenu(menuName = "Config/Effect/" + nameof(FastRunEffectDefinition), fileName = nameof(FastRunEffectDefinition))]
    public sealed class FastRunEffectDefinition : EffectDefinitionAbstract
    {
        [SerializeField] private string _id;
        [SerializeField] private float _power;

        public string Id => _id;
        public float Power => _power;

        public override IEffect GetEffect()
        {
            return new FastRunEffect(this);
        }
    }
}