using System;
using Gameplay.Environment;
using Gameplay.Player.Interfaces;
using Gameplay.Player.Models;
using UI;
using UnityEngine;
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

            // Using compare tag here because it's faster then trying to get the component.
            if (!col.gameObject.CompareTag("Wall") || _playerModel.IsInvincible) return;

            col.gameObject.TryGetComponent(out BlockController blockController);
            blockController.Blink();

            UpdateModel();
            Invoke(nameof(DeathScreen), _playerModel.DeathTimeout);
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