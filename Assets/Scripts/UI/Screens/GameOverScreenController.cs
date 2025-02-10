using Gameplay;
using UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.ServiceLocatorPattern;

namespace UI.Screens
{
    public sealed class GameOverScreenController : ScreenBase
    {
        [SerializeField] private ScoreView _currentScoreView;
        [SerializeField] private ScoreView _maxScoreView;
        [SerializeField] private Button _resetGameButton;
        private IScoreService _scoreService;
        protected override void Init()
        {
            _scoreService = ServiceLocator.Resolve<IScoreService>();
            _resetGameButton.onClick.AddListener(RestartLevel);
        }

        public override void Show()
        {
            base.Show();
            _currentScoreView.SetScore(_scoreService.GetCurrentScore().Current);
            _maxScoreView.SetScore(_scoreService.GetMaxScore());
        }

        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}