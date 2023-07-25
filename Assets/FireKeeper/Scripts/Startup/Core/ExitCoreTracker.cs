using System;
using Cysharp.Threading.Tasks;
using FireKeeper.Config;

namespace Core.Startup
{
    public sealed class ExitCoreTracker : IDisposable
    {
        private readonly IMissionConfig _missionConfig;
        private readonly IMissionDefinition _missionDefinition;

        public ExitCoreTracker(IMissionConfig missionConfig,
            IMissionDefinition missionDefinition)
        {
            _missionConfig = missionConfig;
            _missionDefinition = missionDefinition;
        }

        public void Dispose()
        {
        }

        private void OnUpdateExitCoreHandler()
        {
        }

        private IMissionDefinition GetNextMissionDefinition(string missionDefinitionId)
        {
            if (string.IsNullOrEmpty(missionDefinitionId))
            {
                return _missionConfig.GetNextDefinition(_missionDefinition);
            }

            return _missionConfig.GetDefinition(missionDefinitionId);
        }

        private async UniTask StartExitProcessAsync(IMissionDefinition missionDefinition)
        {
            SwitchToNextMission(missionDefinition).Forget();
        }

        private async UniTask SwitchToNextMission(IMissionDefinition missionDefinition)
        {
        }
    }
}