namespace FireKeeper.Config
{
    public sealed class FastRunEffect : IEffect
    {
        private readonly FastRunEffectDefinition _fastRunEffectDefinition;

        public FastRunEffect(FastRunEffectDefinition fastRunEffectDefinition)
        {
            _fastRunEffectDefinition = fastRunEffectDefinition;
        }

        public void ApplyEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(_fastRunEffectDefinition.Power);
        }

        public void UndoEffect(IPlayerParameters playerParameters)
        {
            playerParameters.ChangeRunSpeed(-_fastRunEffectDefinition.Power);
        }

        public string GetIconKey()
        {
            return _fastRunEffectDefinition.IconKey;
        }

        public float GetTime()
        {
            return _fastRunEffectDefinition.Time;
        }

        public bool IsInfinity()
        {
            return _fastRunEffectDefinition.InfinityTime;
        }
    }
}