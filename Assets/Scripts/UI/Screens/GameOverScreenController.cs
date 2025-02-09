using Gameplay;
using UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.ServiceLocatorPattern;

namespace UI.Screens
{
    public sealed class GameOverScreenController : ScreenBase
    {
        [SerializeField] private ScoreView _currentScoreView;
        [SerializeField] private ScoreView _maxScoreView;
        [SerializeField] private Button _resetGameButton;

        protected override void Init()
        {
            var scoreService = ServiceLocator.Resolve<IScoreService>();
            _currentScoreView.SubscribeToScoreChanges(scoreService.GetCurrentScore());
            _maxScoreView.SetScore(scoreService.GetMaxScore());
            _resetGameButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}