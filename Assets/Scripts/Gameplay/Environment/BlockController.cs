using DG.Tweening;
using UnityEngine;

namespace Gameplay.Environment
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            TryGetComponent(out _spriteRenderer);
        }

        public void Blink()
        {
            _spriteRenderer.DOColor(Color.red, 0.35f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        }

        public void Break()
        {
            Destroy(gameObject);
        }
    }
}