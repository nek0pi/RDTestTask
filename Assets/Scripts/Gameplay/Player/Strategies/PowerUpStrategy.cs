using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Player.Abilities;
using Gameplay.Player.Interfaces;
using Lean.Pool;
using UnityEngine;

namespace Gameplay.Player.Strategies
{
    public class PowerUpStrategy : MonoBehaviour, IPowerUp
    {
        [SerializeField] private List<AbilityBase> _availablePowerUps;
        [SerializeField] private float powerUpDuration = 5f; // Duration before power-down

        public void Init(ICollide collide, PlayerController playerController)
        {
            collide.OnCollide += (obj) => HandlePowerUp(obj, playerController);
            foreach (var ability in _availablePowerUps) ability.Init(playerController);
        }

        private void HandlePowerUp(GameObject obj, PlayerController playerController)
        {
            if (!obj.CompareTag("PowerUp") || _availablePowerUps.Count == 0) return;

            // Remove the power-up object from the scene
            LeanPool.Despawn(obj);

            // Take a random ability from the list and apply it to the player.
            AbilityBase abilityToApply = _availablePowerUps[Random.Range(0, _availablePowerUps.Count)];

            abilityToApply.PowerUp(playerController);

            // This is just some visual feedback to show the player that they have a power-up

            // Start coroutine to power down after N seconds
            StartCoroutine(PowerDownAfterDelay(abilityToApply, playerController, powerUpDuration));
        }

        private IEnumerator PowerDownAfterDelay(AbilityBase ability, PlayerController player, float delay)
        {
            yield return new WaitForSeconds(delay);
            ability.PowerDown(player);
        }
    }
}