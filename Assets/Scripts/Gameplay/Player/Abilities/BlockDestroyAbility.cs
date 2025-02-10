using DG.Tweening;
using Gameplay.Environment;
using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public sealed class BlockDestroyAbility : AbilityBase
    {
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;

        private void DestroyBlock(GameObject block)
        {
            if (!block.TryGetComponent(out BlockController blockController)) return;
            blockController.Break();
        }

        public override void Init(PlayerController playerController)
        {
            playerController.TryGetComponent(out _spriteRenderer);
            _originalColor = _spriteRenderer.color;
        }

        public override void PowerUp(PlayerController playerController)
        {
            playerController.Collider.OnCollide += DestroyBlock;
            _spriteRenderer.color = Color.green;
            playerController.Model.IsInvincible = true;
        }

        public override void PowerDown(PlayerController playerController)
        {
            playerController.Collider.OnCollide -= DestroyBlock;
            _spriteRenderer.color = _originalColor;
            playerController.Model.IsInvincible = false;
        }
    }
}