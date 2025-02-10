using System;
using System.Collections.Generic;
using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour, IUIService
    {
        [SerializeField] private Dictionary<ScreenType, ScreenBase> _screens = new();
        private ScreenBase _currentScreen;

        // TODO Use an Odin Ispector or serialize the dictionary in a different way
        [SerializeField] private ScreenBase _menuScreen;
        [SerializeField] private ScreenBase _gameplayScreen;
        [SerializeField] private ScreenBase _gameOverScreen;

        private void Start()
        {
            _screens.Add(ScreenType.Menu, _menuScreen);
            _screens.Add(ScreenType.Gameplay, _gameplayScreen);
            _screens.Add(ScreenType.GameOver, _gameOverScreen);

            _currentScreen = _gameplayScreen;
        }

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

        public ScreenBase GetScreen(ScreenType gameplay)
        {
            if (_screens.ContainsKey(gameplay)) return _screens[gameplay];
            Debug.LogWarning($"Screen of type {gameplay} not found in the dictionary");
            return null;
        }
    }
}