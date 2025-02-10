using Gameplay;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.ServiceLocatorPattern;

namespace UI.Screens
{
    public sealed class GameplayScreenController : ScreenBase
    {
        [SerializeField] private InputSliderView _inputSliderView;
        [SerializeField] private ScoreView _currentScoreView;
        [SerializeField] private Button _menuButton;

        protected override void Init()
        {
            _currentScoreView.SubscribeToScoreChanges(ServiceLocator.Resolve<IScoreService>().GetCurrentScore());
            _menuButton.onClick.AddListener(() => ServiceLocator.Resolve<IUIService>().SwitchToScreen(ScreenType.Menu));
        }

        public ReactiveFloat GetSliderValue()
        {
            return _inputSliderView.GetSliderValue();
        }
    }
}