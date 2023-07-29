using FireKeeper.Config;
using UnityEngine;

namespace FireKeeper.Core.Engine
{
    public sealed class PlayerParameters : IPlayerParameters
    {
        private readonly IPlayerDefinition _playerDefinition;
        
        private float _stepSpeed;
        private float _runSpeed;
        private float _currentStamina;
        private float _maxStamina;
        
        public PlayerParameters(IPlayerDefinition playerDefinition)
        {
            _playerDefinition = playerDefinition;
            _stepSpeed = playerDefinition.StepSpeed;
            _runSpeed = playerDefinition.RunSpeed;
            _maxStamina = playerDefinition.Stamina;
            _currentStamina = _maxStamina;
        }

        public void ChangeStepSpeed(float amount) => _stepSpeed += amount;
        public void ChangeRunSpeed(float amount) => _runSpeed += amount;
        public void ChangeMaxStamina(float amount)
        {
            _maxStamina += amount;
            if (_currentStamina > _maxStamina)
                _currentStamina = _maxStamina;
        }

        public void ChangeStamina(int amount)
        {
            _currentStamina = Mathf.Max(0, _currentStamina + amount);
        }

        public float GetStepSpeed()
        {
            if (_stepSpeed < 0)
                return 0;

            return _stepSpeed;
        }
        
        public float GetRunSpeed()
        {
            if (_runSpeed < 0)
                return 0;

            return _runSpeed;
        }
        
        public float GetStaminaSpeed()
        {
            if (_maxStamina < 0)
                return 0;

            return _maxStamina;
        }
    }
}