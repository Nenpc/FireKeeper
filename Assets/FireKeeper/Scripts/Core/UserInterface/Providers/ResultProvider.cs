using System;
using FireKeeper.Core.Engine;
using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class ResultProvider : IDisposable
    {
        private readonly IProgressController _progressController;
        private readonly IWindowFacade _windowFacade;
        
        public ResultProvider(IProgressController progressController, IWindowFacade windowFacade)
        {
            _progressController = progressController;
            _windowFacade = windowFacade;
            _progressController.WinAction += ShowWinWindow;
            _progressController.DefeatAction += ShowDefeatWindow;
        }

        public void Dispose()
        {
            _progressController.WinAction -= ShowWinWindow;
        }
        
        private void ShowWinWindow()
        {
            _windowFacade.ShowAsync<WinPopupView>();
        }
        
        private void ShowDefeatWindow()
        {
            _windowFacade.ShowAsync<DefeatPopupView>();
        }
    }
}