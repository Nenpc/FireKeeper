namespace FireKeeper.Config
{
    public sealed class SlowPlayerEffect : EffectAbstract
    {
        private readonly SlowEffectDefinition _slowEffectDefinition;
        
        protected override EffectDefinitionAbstract EffectDefinition => _slowEffectDefinition;

        public SlowPlayerEffect(SlowEffectDefinition slowEffectDefinition)
        {
            _slowEffectDefinition = slowEffectDefinition;
        }

        public override void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(-_slowEffectDefinition.Power);
            playerParameters.ChangeStepSpeed(-_slowEffectDefinition.Power);
        }

        public override void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(_slowEffectDefinition.Power);
            playerParameters.ChangeStepSpeed(_slowEffectDefinition.Power);
        }
    }
}