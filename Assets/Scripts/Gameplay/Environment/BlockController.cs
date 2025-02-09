using DG.Tweening;
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
            _spriteRenderer.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}