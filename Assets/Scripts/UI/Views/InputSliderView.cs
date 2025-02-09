using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI.Views
{
    public class InputSliderView : MonoBehaviour, IPointerDownHandler
    {
        // Slider that reads the input from the player from left to right
        private ReactiveFloat _sliderValue;
        private RectTransform _sliderRect;

        private void Start()
        {
            _sliderValue = new ReactiveFloat();
            // Search for sliderRect on this GameObject using TryGetComponent
            if (!TryGetComponent(out _sliderRect))
            {
                Debug.LogError("SliderRect not found on this GameObject");
            }
        }

        public ReactiveFloat GetSliderValue()
        {
            return _sliderValue;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Calculate the position of the cursor relative to the RectTransform's width
            _sliderValue.Current = eventData.position.x / _sliderRect.rect.width;
        }
    }
}