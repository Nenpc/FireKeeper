namespace FireKeeper.Config
{
    public sealed class FastRunEffect : EffectAbstract
    {
        private readonly FastRunEffectDefinition _fastRunEffectDefinition;
        
        protected override EffectDefinitionAbstract EffectDefinition => _fastRunEffectDefinition;

        public FastRunEffect(FastRunEffectDefinition fastRunEffectDefinition)
        {
            _fastRunEffectDefinition = fastRunEffectDefinition;
        }
        
        public override void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(_fastRunEffectDefinition.Power);
        }

        public override void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(-_fastRunEffectDefinition.Power);
        }
    }
}