namespace FireKeeper.Config
{
    public sealed class StaminaDamageEffect : EffectAbstract
    {
        private readonly StaminaEffectDefinition _staminaDamageDefinition;
        
        protected override EffectDefinitionAbstract EffectDefinition => _staminaDamageDefinition;

        public StaminaDamageEffect(StaminaEffectDefinition staminaDamageDefinition)
        {
            _staminaDamageDefinition = staminaDamageDefinition;
        }

        public override void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeMaxStamina(-_staminaDamageDefinition.Power);
        }

        public override void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeMaxStamina(_staminaDamageDefinition.Power);
        }
    }
}