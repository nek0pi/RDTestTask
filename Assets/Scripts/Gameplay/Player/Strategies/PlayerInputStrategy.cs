using Gameplay.Player.Interfaces;
using UI;
using UI.Screens;
using Utils;
using Utils.ServiceLocatorPattern;

namespace Gameplay.Player.Strategies
{
    public sealed class PlayerInputStrategy : IInput
    {
        private GameplayScreenController _gameplayScreenController;

        public void Init()
        {
            ServiceLocator.Resolve<IUIService>().GetScreen(ScreenType.Gameplay)
                .TryGetComponent(out _gameplayScreenController);
        }

        public ReactiveFloat GetX()
        {
            return _gameplayScreenController.GetSliderValue();
        }
    }
}