using System.Collections.Generic;
using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour, IUIService
    {
        [SerializeField] private Dictionary<ScreenType, ScreenBase> _screens;
        private ScreenBase _currentScreen;

        public void SwitchToScreen(ScreenType screenType)
        {
            _currentScreen.Hide();

            if (!_screens.ContainsKey(screenType))
            {
                Debug.LogError($"Screen of type {screenType} not found in the dictionary");
                return;
            }

            _currentScreen = _screens[screenType];
            _currentScreen.Show();
        }

        public void HideScreen(ScreenType screenType)
        {
            _currentScreen.Hide();
        }
    }
}