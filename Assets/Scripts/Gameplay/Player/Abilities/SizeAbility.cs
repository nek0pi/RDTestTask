using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public sealed class SizeAbility : AbilityBase
    {
        private void Grow(Transform playerTransform)
        {
            playerTransform.localScale = new Vector3(2, 2, 2);
        }

        private void Shrink(Transform playerTransform)
        {
            playerTransform.localScale = new Vector3(1, 1, 1);
        }

        public override void PowerUp(PlayerController playerController)
        {
            Grow(playerController.transform);
        }

        public override void PowerDown(PlayerController playerController)
        {
            Shrink(playerController.transform);
        }
    }
}