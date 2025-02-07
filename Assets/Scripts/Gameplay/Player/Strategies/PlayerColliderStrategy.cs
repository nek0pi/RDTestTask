using System;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UnityEngine;

namespace Gameplay.Player.Strategies
{
    public sealed class PlayerColliderStrategy : MonoBehaviour, ICollide
    {
        private PlayerModel _playerModel;

        public event Action<GameObject> OnCollide;

        public void Init(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            OnCollide?.Invoke(col.gameObject);

            if (!col.gameObject.TryGetComponent(out BlockController blockController) ||
                _playerModel.IsInvincible) return;
            blockController.Blink();

            DeathLock();

            Invoke(nameof(DeathScreen), _playerModel.DeathTimeout);

            // TODO What if there is a powerup and you also die?
        }

        private void DeathLock()
        {
            _playerModel.IsMovable = false;
            // Call GameOver sequence
        }

        private void DeathScreen()
        {
            // TODO UI Controller to show GameOver screen.
        }
    }
}