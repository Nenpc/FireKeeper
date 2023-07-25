using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class MenuPopupController : WindowController<MenuPopupView>
    {
        public MenuPopupController(IWindowFacade windowFacade) : base(windowFacade)
        {
        }
    }
}