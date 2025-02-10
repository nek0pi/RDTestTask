using System;
using Data;
using Gameplay.Player;
using UnityEngine;
using Utils;
using Utils.ServiceLocatorPattern;

namespace Gameplay
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        private int _maxScore;
        private ReactiveInt _currentScore = new ReactiveInt();
        [SerializeField] private PlayerController _playerController;
        private IProgressService _progressService;

        [SerializeField]
        private float
            pointInterval = 0.5f; // Improvement: This is time in seconds for 1 point, could be moved to config instead

        private float _timer = 0f;

        private void Start()
        {
            _progressService = ServiceLocator.Resolve<IProgressService>();
            _maxScore = _progressService.GetMaxScore();
        }

        private void Update()
        {
            if (_playerController == null || _playerController.Model == null) return;
            CheckMaxScore();
            if (!_playerController.Model.IsMovable) return;

            _timer += Time.deltaTime;

            // Check if pointInterval seconds have passed
            if (!(_timer >= pointInterval)) return;

            _currentScore.Current += 1;
            _timer -= pointInterval; // Reset the timer, keeping leftover time
        }

        // This could be improved with the model having a ReactiveBool instead.
        private void CheckMaxScore()
        {
            if (!_playerController.Model.IsDead) return;
            // Check if this is the new max score. If yes -> save it

            if (_currentScore.Current <= _maxScore) return;

            _maxScore = _currentScore.Current;
            _progressService.SetMaxScore(_maxScore);
        }

        public int GetMaxScore()
        {
            return _maxScore;
        }

        public ReactiveInt GetCurrentScore()
        {
            return _currentScore;
        }
    }
}