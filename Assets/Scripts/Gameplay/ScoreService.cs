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

        [SerializeField] private float pointInterval = 0.5f; // TODO Time in seconds for 1 point -> Move to Config
        private float _timer = 0f;

        private void Start()
        {
            _progressService = ServiceLocator.Resolve<IProgressService>();
            _maxScore = _progressService.GetMaxScore();
        }

        private void Update()
        {
            if (_playerController == null || _playerController.Model == null) return;
            if (_playerController.Model.IsDead || _playerController.Model.IsMovable == false) return;
            _timer += Time.deltaTime;

            // Check if pointInterval seconds have passed
            if (!(_timer >= pointInterval)) return;

            _currentScore.Current += 1;
            _timer -= pointInterval; // Reset the timer, keeping leftover time
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