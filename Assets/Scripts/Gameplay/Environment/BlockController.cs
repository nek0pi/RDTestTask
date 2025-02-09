using System;
using UnityEngine;

namespace Gameplay.Environment
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            TryGetComponent(out _spriteRenderer);
        }

        public void Blink()
        {
            // TODO Blink the block using DOTween.
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}