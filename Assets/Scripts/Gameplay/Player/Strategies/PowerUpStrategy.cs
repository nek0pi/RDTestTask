using Gameplay.Player.Abilities;
using Gameplay.Player.Interfaces;
using UnityEngine;

namespace Gameplay.Player.Strategies
{
    public class PowerUpStrategy : MonoBehaviour, IPowerUp
    {
        private PlayerController _playerController;

        public void Init(ICollide collide, PlayerController playerController)
        {
            collide.OnCollide += (obj) => HandlePowerUp(obj, playerController);
        }

        private void HandlePowerUp(GameObject obj, PlayerController playerController)
        {
            if (!obj.TryGetComponent(out AbilityBase abilityBase)) return;

            abilityBase.PowerUp(playerController);
            // TODO Call abilityBase.PowerDown(playerController) after N amount of seconds.
            
            abilityBase.PowerDown(playerController);
        }
    }
}