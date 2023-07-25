using System;

namespace FireKeeper.Core.Engine
{
    public interface ICoreTimeController
    {
        event Action<float> TickAction;
        public void Stop();
        public void Start();
        public float GetLeftTime();
    }
}