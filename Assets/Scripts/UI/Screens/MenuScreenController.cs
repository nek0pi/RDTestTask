using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.ServiceLocatorPattern;

namespace UI.Screens
{
    public sealed class MenuScreenController : ScreenBase
    {
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _continueGameButton;

        protected override void Init()
        {
            _resetGameButton.onClick.AddListener(RestartLevel);
            _continueGameButton.onClick.AddListener(ContinueGame);
        }

        public override void Show()
        {
            base.Show();
            // Pause the game
            Time.timeScale = 0;
        }

        public override void Hide()
        {
            base.Hide();
            // Unpause the game
            Time.timeScale = 1;
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void ContinueGame() => ServiceLocator.Resolve<IUIService>().SwitchToScreen(ScreenType.Gameplay);
    }
}