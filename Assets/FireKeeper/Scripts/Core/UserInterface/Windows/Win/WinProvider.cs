using System;
using FireKeeper.Core.Engine;
using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class WinProvider : IDisposable
    {
        private readonly IWinController _winController;
        private readonly IWindowFacade _windowFacade;
        
        public WinProvider(IWinController winController, IWindowFacade windowFacade)
        {
            _winController = winController;
            _windowFacade = windowFacade;
            _winController.WinAction += ShowWinWindow;
        }

        public void Dispose()
        {
            _winController.WinAction -= ShowWinWindow;
        }
        
        private void ShowWinWindow()
        {
            _windowFacade.ShowAsync<WinPopupView>();
        }
    }
}