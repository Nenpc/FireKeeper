namespace FireKeeper.Config
{
    public interface IPlayerParameters
    {
        void ChangeStepSpeed(float amount);
        void ChangeRunSpeed(float amount);
        void ChangeMaxStamina(float amount);
        void ChangeStamina(int amount);
        float GetStepSpeed();
        float GetRunSpeed();
        float GetStaminaSpeed();
    }
}