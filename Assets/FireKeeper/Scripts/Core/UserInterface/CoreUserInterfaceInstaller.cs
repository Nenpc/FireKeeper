using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreUserInterfaceInstaller : WindowInstaller
    {
        private int _systemOrder = 1000;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInterfacesTo<CoreUserInterfaceExtraInitializer>().AsSingle();
            
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<TextureProvider>().AsSingle();
            Container.BindInterfacesTo<CoreHudController>().AsSingle();
            Container.BindInterfacesTo<MenuPopupController>().AsSingle();
            Container.BindInterfacesTo<WinPopupController>().AsSingle();
            
            Container.BindInterfacesTo<WinProvider>().AsSingle();
        }
    }
}