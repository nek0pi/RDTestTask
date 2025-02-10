using Gameplay.Player.Abilities.Configs;
using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public sealed class SizeAbility : AbilityBase
    {
        [SerializeField] private SizeAbilityConfigSO _sizeAbilityConfig;

        private void Grow(Transform playerTransform)
        {
            playerTransform.localScale = new Vector3(_sizeAbilityConfig.OriginalSize, _sizeAbilityConfig.OriginalSize,
                _sizeAbilityConfig.OriginalSize);
        }

        private void Shrink(Transform playerTransform)
        {
            playerTransform.localScale = new Vector3(_sizeAbilityConfig.ShrinkSize, _sizeAbilityConfig.ShrinkSize,
                _sizeAbilityConfig.ShrinkSize);
        }

        public override void PowerUp(PlayerController playerController)
        {
            Shrink(playerController.transform);
        }

        public override void PowerDown(PlayerController playerController)
        {
            Grow(playerController.transform);
        }
    }
}