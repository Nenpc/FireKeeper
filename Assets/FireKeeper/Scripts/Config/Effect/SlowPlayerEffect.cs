namespace FireKeeper.Config
{
    public sealed class SlowPlayerEffect : IEffect
    {
        private readonly SlowEffectDefinition _slowEffectDefinition;

        public SlowPlayerEffect(SlowEffectDefinition slowEffectDefinition)
        {
            _slowEffectDefinition = slowEffectDefinition;
        }

        public void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(-_slowEffectDefinition.Power);
            playerParameters.ChangeStepSpeed(-_slowEffectDefinition.Power);
        }

        public void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(_slowEffectDefinition.Power);
            playerParameters.ChangeStepSpeed(_slowEffectDefinition.Power);
        }
        
        public string GetIconKey()
        {
            return _slowEffectDefinition.IconKey;
        }

        public float GetTime()
        {
            return _slowEffectDefinition.Time;
        }

        public bool IsInfinity()
        {
            return _slowEffectDefinition.InfinityTime;
        }
    }
}