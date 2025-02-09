using Data.Models;
using UnityEngine;
using Utils;
using Utils.ServiceLocatorPattern;

namespace Data
{
    public class ProgressService : MonoBehaviour, IProgressService
    {
        private ISaveLoadService _saveLoadService;
        private GameProgressModel _progressModelCache;

        private void Start()
        {
            _saveLoadService = ServiceLocator.Resolve<ISaveLoadService>();
        }

        public int GetMaxScore()
        {
            _progressModelCache = _saveLoadService.LoadSync();
            return _progressModelCache.MaxScore;
        }

        public void SetMaxScore(int score)
        {
            _progressModelCache.MaxScore = score;
            _saveLoadService.Save(_progressModelCache);
        }
    }
}