using System;
using FireKeeper.Config;

namespace FireKeeper.Core.Engine
{
    public sealed class ProgressController : IProgressController, IDisposable
    {
        public event Action<float> ProgressAction;
        public event Action WinAction;
        public event Action DefeatAction;

        private readonly ICoreTimeController _coreTimeController;
        private readonly IMissionConfig _missionConfig;
        private readonly IBonfireController _bonfireController;
        private readonly int _winTime;

        public ProgressController(ICoreTimeController coreTimeController,
            IMissionConfig missionConfig,
            IBonfireController bonfireController)
        {
            _coreTimeController = coreTimeController;
            _missionConfig = missionConfig;
            _bonfireController = bonfireController;
            
            _winTime = _missionConfig.GetFirstDefinition().WinTime;
            
            _coreTimeController.TickAction += UpdateTime;
            _bonfireController.GoOutAction += Defeat;
        }
        
        public void Dispose()
        {
            _coreTimeController.TickAction -= UpdateTime;
            _bonfireController.GoOutAction += Defeat;
        }

        private void Defeat()
        {
            DefeatAction?.Invoke();
            _coreTimeController.Stop();
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