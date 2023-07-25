using Zenject;

namespace Core.Startup
{
    public sealed class StartupCoreExtraInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ExitCoreTracker>().AsSingle();
            
            Container.BindInterfacesTo<StartupCoreExtraInitializer>().AsSingle();
            Container.BindInterfacesTo<LoadingScreenExtraInitializer>().AsSingle();
        }
    }
}