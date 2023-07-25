namespace FireKeeper.Config
{
    public sealed class StaminaDamageEffect : IEffect
    {
        private readonly StaminaEffectDefinition _staminaDamageDefinition;

        public StaminaDamageEffect(StaminaEffectDefinition staminaDamageDefinition)
        {
            _staminaDamageDefinition = staminaDamageDefinition;
        }

        public void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeMaxStamina(-_staminaDamageDefinition.Power);
        }

        public void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeMaxStamina(_staminaDamageDefinition.Power);
        }
        
        public string GetIconKey()
        {
            return _staminaDamageDefinition.IconKey;
        }
        
        public float GetTime()
        {
            return _staminaDamageDefinition.Time;
        }

        public bool IsInfinity()
        {
            return _staminaDamageDefinition.InfinityTime;
        }
    }
}