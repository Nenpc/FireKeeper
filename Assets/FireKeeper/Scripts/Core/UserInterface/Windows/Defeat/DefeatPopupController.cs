using GameLib.Window;

namespace FireKeeper.Core.UserInterface
{
    public sealed class DefeatPopupController : WindowController<DefeatPopupView>
    {
        public DefeatPopupController(IWindowFacade windowFacade) : base(windowFacade)
        {
        }
    }
}