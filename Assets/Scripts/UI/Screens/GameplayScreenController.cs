using Gameplay;
using UI.Views;
using UnityEngine;
using Utils;
using Utils.ServiceLocatorPattern;

namespace UI.Screens
{
    public sealed class GameplayScreenController : ScreenBase
    {
        [SerializeField] private InputSliderView _inputSliderView;
        [SerializeField] private ScoreView _currentScoreView;

        public ReactiveFloat GetSliderValue()
        {
            return _inputSliderView.GetSliderValue();
        }

        protected override void Init()
        {
            _currentScoreView.SubscribeToScoreChanges(ServiceLocator.Resolve<IScoreService>().GetCurrentScore());
        }
    }
}