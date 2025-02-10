using UnityEngine;

namespace UI.Screens
{
    public abstract class ScreenBase : MonoBehaviour
    {
        [SerializeField] protected GameObject _screenRoot;

        private void Start()
        {
            Init();
        }

        protected abstract void Init();

        public virtual void Show()
        {
            _screenRoot.SetActive(true);
        }

        public virtual void Hide()
        {
            _screenRoot.SetActive(false);
        }
    }
}