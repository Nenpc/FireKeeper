namespace FireKeeper.Config
{
    public abstract class EffectAbstract : IEffect
    {
        protected abstract EffectDefinitionAbstract EffectDefinition { get; }
        public abstract void ApplyEffect(IPlayerParameters playerParameters);
        public abstract void UndoEffect(IPlayerParameters playerParameters);
        public string GetIconKey() => EffectDefinition.IconKey;
        public float GetTime() => EffectDefinition.Time;
        public bool IsInfinity() => EffectDefinition.InfinityTime;
        public bool IsGoodEffect() => EffectDefinition.IsGoodEffect;
    }
}