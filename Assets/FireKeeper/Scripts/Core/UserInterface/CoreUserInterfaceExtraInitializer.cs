using Cysharp.Threading.Tasks;
using FireKeeper.Core.Engine;
using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class CoreUserInterfaceExtraInitializer : ICoreExtraInitializer
    {
        public int Order => 100;
        
        private readonly IWindowFacade _windowFacade;

        public CoreUserInterfaceExtraInitializer(IWindowFacade windowFacade)
        {
            _windowFacade = windowFacade;
        }

        public async UniTask InitializeAsync()
        {
            await _windowFacade.InitializeAsync();
        }
    }
}