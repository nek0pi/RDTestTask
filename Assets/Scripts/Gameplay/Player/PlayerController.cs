using System;
using Gameplay.Player.Configs;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        // TODO Encapsulate collider to be readable from outside, and assignable from inside
        public ICollide Collider;
        private IMove _mover;
        private IInput _input;
        private PlayerModel _playerModel;

        [SerializeField] private PlayerConfigSO _playerConfig;

        // Start is called before the first frame update
        void Awake()
        {
            // Search for strategies with TryGetComponent.
            if (!TryGetComponent(out _mover)) throw new NullReferenceException("Mover not found.");
            if (!TryGetComponent(out Collider)) throw new NullReferenceException("Collider not found.");
            if (!TryGetComponent(out _input)) throw new NullReferenceException("Input not found.");

            // Initialize the player model with the PlayerConfig.
            _playerModel = new PlayerModel(_playerConfig);
        }
    }

    public sealed class PlayerInputStrategy : IInput
    {
        // Reads from UIController's GameplayScreenController X input.
        public float GetX()
        {
            // TODO  Go to UIController and ask for GameplayScreenController X input.
            throw new NotImplementedException();
        }
    }

    public class BlockController : MonoBehaviour
    {
        public void Blink()
        {
            // TODO Blink the block.
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }

    public sealed class PlayerMoveStrategy : MonoBehaviour, IMove
    {
        private PlayerModel _playerModel;
        private IInput _inputer;

        public void Init(PlayerModel playerModel, IInput input)
        {
            _playerModel = playerModel;
        }

        private void Update()
        {
            if (!_playerModel.IsMovable) return;

            // Move the player.
            transform.position += new Vector3(_inputer.GetX(), _playerModel.MoveYSpeed, 0);
        }
    }
}