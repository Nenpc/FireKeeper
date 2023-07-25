using Cysharp.Threading.Tasks;
using FireKeeper.Config;
using Zenject;

namespace Core.Startup
{
    public sealed class StartupSceneInitializer : IInitializable
    {
        private readonly IMissionConfig _missionConfig;
        private readonly ISceneSwitcher _sceneSwitcher;

        public StartupSceneInitializer(IMissionConfig missionConfig,
            ISceneSwitcher sceneSwitcher)
        {
            _missionConfig = missionConfig;
            _sceneSwitcher = sceneSwitcher;
        }

        public void Initialize()
        {
            RunAsync().Forget();
        }

        private async UniTask RunAsync()
        {
            await SwitchToCore();
        }

        private async UniTask SwitchToCore()
        {
            var  missionDefinition = _missionConfig.GetFirstDefinition();
            await _sceneSwitcher.SwitchToCoreSceneAsync(missionDefinition);
        }
    }
}