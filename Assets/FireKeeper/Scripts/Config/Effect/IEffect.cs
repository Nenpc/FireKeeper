namespace FireKeeper.Config
{
    public interface IEffect
    {
        void ApplyEffect(IPlayerParameters playerParameters);
        void UndoEffect(IPlayerParameters playerParameters);
        string GetIconKey();
        float GetTime();
        bool IsInfinity();
        bool IsGoodEffect();
    }
}