using Cysharp.Threading.Tasks;
using FireKeeper.Config;

namespace Core.Startup
{
    public interface ISceneSwitcher
    {
        UniTask SwitchToCoreSceneAsync(IMissionDefinition missionDefinition);
        UniTask SwitchToStartupSceneAsync();
    }
}