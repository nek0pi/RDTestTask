using System;
using Gameplay.Environment;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UI;
using UnityEngine;
using Utils;
using Utils.ServiceLocatorPattern;

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

            UpdateModel();
            Invoke(nameof(DeathScreen), _playerModel.DeathTimeout);

            // BUG Potentially: What if there is a PowerUp and you also die?
        }

        private void UpdateModel()
        {
            _playerModel.IsMovable = false;
            _playerModel.IsDead = true;
        }

        private void DeathScreen()
        {
            ServiceLocator.Resolve<IUIService>().SwitchToScreen(ScreenType.GameOver);
        }
    }
}