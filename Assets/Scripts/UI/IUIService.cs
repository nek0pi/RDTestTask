namespace UI
{
    public interface IUIService
    {
        void SwitchToScreen(ScreenType screenType);
        void HideScreen(ScreenType screenType);
    }
}