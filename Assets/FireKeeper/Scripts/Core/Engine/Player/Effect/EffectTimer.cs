using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public sealed class EffectTimer
    {
        private readonly IEffect _effect;
        public IEffect GetEffect() => _effect;

        private float _leftTime;

        public EffectTimer(IEffect effect)
        {
            _effect = effect;
            _leftTime = _effect.GetTime();
        }

        public float GetLeftTime() => _leftTime;
        public float GetMaxLeftTime() => _effect.GetTime();

        public bool ReduceTime(float deltaTime)
        {
            _leftTime -= deltaTime;
            return TimeEnd();
        }

        public bool TimeEnd()
        {
            if (_effect.IsInfinity())
            {
                return false;
            }
            else
            {
                return _leftTime < 0;
            }
        }
    }
}