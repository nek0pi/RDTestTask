using Data;
using Gameplay;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Utils.ServiceLocatorPattern
{
    public class ProdServicesContainer : MonoBehaviour, ISerivceContainer
    {
        [SerializeField] private ProgressService _progressService;

        [SerializeField] private ScoreService _scoreService;
        [SerializeField] private UIService _uiService;

        [SerializeField] private PlayerPrefsSaveLoadService _playerPrefsSaveLoadService;

        public void Bind()
        {
            AssignAllServices();
            ServiceLocator.Register<IProgressService>(_progressService);
            ServiceLocator.Register<IScoreService>(_scoreService);
            ServiceLocator.Register<IUIService>(_uiService);
            ServiceLocator.Register<ISaveLoadService>(_playerPrefsSaveLoadService);
        }

        private void AssignAllServices()
        {
            if (TryGetComponent(out _progressService) == false)
                _progressService = this.AddComponent<ProgressService>();
            if (TryGetComponent(out _scoreService) == false)
                _scoreService = this.AddComponent<ScoreService>();
            if (TryGetComponent(out _uiService) == false)
                _uiService = this.AddComponent<UIService>();
            if (TryGetComponent(out _playerPrefsSaveLoadService) == false)
                _playerPrefsSaveLoadService = this.AddComponent<PlayerPrefsSaveLoadService>();
        }
    }
}