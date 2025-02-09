using System;
using Gameplay.Player.Configs;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public ICollide Collider => _collider;
        public PlayerModel Model => _playerModel;
        [SerializeField] private PlayerConfigSO _playerConfig;

        private ICollide _collider;
        private IMove _mover;
        private IInput _input;
        private IPowerUp _powerUp;
        private PlayerModel _playerModel;


        // Start is called before the first frame update
        void Awake()
        {
            // Search for strategies with TryGetComponent.
            if (!TryGetComponent(out _mover)) throw new NullReferenceException("Mover not found.");
            if (!TryGetComponent(out _collider)) throw new NullReferenceException("Collider not found.");
            if (!TryGetComponent(out _input)) throw new NullReferenceException("Input not found.");
            if (!TryGetComponent(out _powerUp)) throw new NullReferenceException("PowerUp not found.");

            // Initialize the player model with the PlayerConfig.
            _playerModel = new PlayerModel(_playerConfig);

            // Initialize the strategies.
            _mover.Init(_playerModel, _input);
            _collider.Init(_playerModel);
            _input.Init();
            _powerUp.Init(_collider, this);
        }
    }
}