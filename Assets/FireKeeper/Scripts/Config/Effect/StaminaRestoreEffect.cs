namespace FireKeeper.Config
{
    public sealed class StaminaRestoreEffect : EffectAbstract
    {
        private readonly StaminaRestoreEffectDefinition _staminaRestoreEffectDefinition;

        protected override EffectDefinitionAbstract EffectDefinition => _staminaRestoreEffectDefinition;
        
        public StaminaRestoreEffect(StaminaRestoreEffectDefinition staminaRestoreEffectDefinition)
        {
            _staminaRestoreEffectDefinition = staminaRestoreEffectDefinition;
        }

        public override void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeStamina(_staminaRestoreEffectDefinition.Power);
        }

        public override void UndoEffect(IPlayerParameters playerParameters)
        {
            return;
        }
    }
}