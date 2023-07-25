using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class WinPopupController : WindowController<WinPopupView>
    {
        public WinPopupController(IWindowFacade windowFacade) : base(windowFacade)
        {
        }
    }
}