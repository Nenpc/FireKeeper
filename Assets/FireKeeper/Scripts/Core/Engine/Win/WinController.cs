using System;
using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public sealed class WinController : IWinController, IDisposable
    {
        public event Action<float> ProgressAction;
        public event Action WinAction;

        private readonly ICoreTimeController _coreTimeController;
        private readonly IMissionConfig _missionConfig;
        private readonly int _winTime;

        public WinController(ICoreTimeController coreTimeController, 
            IMissionConfig missionConfig)
        {
            _coreTimeController = coreTimeController;
            _missionConfig = missionConfig;
            _winTime = _missionConfig.GetFirstDefinition().WinTime;
            _coreTimeController.TickAction += UpdateTime;
        }

        public void Dispose()
        {
            _coreTimeController.TickAction -= UpdateTime;
        }

        private void UpdateTime(float deltaTime)
        {
            var leftTime = _coreTimeController.GetLeftTime();
            ProgressAction?.Invoke(leftTime);

            if (leftTime >= _winTime)
            {
                WinAction?.Invoke();
                _coreTimeController.Stop();
            }
        }

        public int GetWinTime()
        {
            return _winTime;
        }
    }
}