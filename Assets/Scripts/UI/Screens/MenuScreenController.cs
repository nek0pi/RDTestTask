using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

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

        private void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void ContinueGame() => ServiceLocator.Resolve<UIService>().SwitchToScreen(ScreenType.Gameplay);
    }
}