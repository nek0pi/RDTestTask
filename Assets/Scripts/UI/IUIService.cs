using UI.Screens;

namespace UI
{
    public interface IUIService
    {
        void SwitchToScreen(ScreenType screenType);
        void HideScreen(ScreenType screenType);
        ScreenBase GetScreen(ScreenType gameplay);
    }
}