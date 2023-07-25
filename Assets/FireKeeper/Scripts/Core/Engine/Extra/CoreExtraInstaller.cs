using FireKeeper.Config;
using Zenject;

namespace FireKeeper.Core.Engine
{
    public sealed class CoreExtraInstaller : Installer
    {
        private readonly IMissionDefinition _missionDefinition;

        public CoreExtraInstaller(IMissionDefinition missionDefinition)
        {
            _missionDefinition = missionDefinition;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_missionDefinition).AsSingle();

            Container.BindInterfacesTo<CoreEngineExtraInitializer>().AsSingle();
        }
    }
}