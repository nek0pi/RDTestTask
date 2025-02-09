using System;
using Gameplay.Environment;
using UnityEngine;

namespace Gameplay.Player.Abilities
{
    public sealed class BlockDestroyAbility : AbilityBase
    {
        private void DestroyBlock(GameObject block)
        {
            if (!block.TryGetComponent(out BlockController blockController)) return;
            blockController.Break();
        }

        public override void PowerUp(PlayerController playerController)
        {
            playerController.Collider.OnCollide += DestroyBlock;
        }

        public override void PowerDown(PlayerController playerController)
        {
            playerController.Collider.OnCollide -= DestroyBlock;
        }
    }
}