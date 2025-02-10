using System.Collections.Generic;
using Gameplay.Player.Abilities;
using Gameplay.Player.Interfaces;
using UnityEngine;

namespace Gameplay.Player.Strategies
{
    public class PowerUpStrategy : MonoBehaviour, IPowerUp
    {
        private PlayerController _playerController;
        [SerializeField] List<AbilityBase> _availablePowerUps;
        public void Init(ICollide collide, PlayerController playerController)
        {
            collide.OnCollide += (obj) => HandlePowerUp(obj, playerController);
        }

        private void HandlePowerUp(GameObject obj, PlayerController playerController)
        {
            if (!obj.CompareTag("PowerUp")) return;
            
            // Take a random ability from the list and apply it to the player.
            AbilityBase abilityToApply = _availablePowerUps[Random.Range(0, _availablePowerUps.Count)];

            abilityToApply.PowerUp(playerController);
            // TODO Call abilityBase.PowerDown(playerController) after N amount of seconds.
            
            abilityToApply.PowerDown(playerController);
        }
    }
}