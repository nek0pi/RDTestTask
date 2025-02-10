using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI.Views
{
    public class InputSliderView : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        // Slider that reads the input from the player from left to right
        private ReactiveFloat _sliderValue;
        private RectTransform _sliderRect;
        private bool _isTracking;

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
            UpdateSliderValue(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateSliderValue(eventData);
        }

        private void UpdateSliderValue(PointerEventData eventData)
        {
            // Get the position of the pointer for 0 to 1 from left to right of the screen
            // _sliderValue.Current = eventData.position.x / Screen.width;
            Camera cam = eventData.pressEventCamera;
            // Get screen position
            Vector3 screenPoint = eventData.position;

            // Convert to world space using a fixed distance (adjust based on gameplay needs)
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, cam.nearClipPlane));

            // Use the world X position for slider value
            _sliderValue.Current = worldPoint.x;
        }
    }
}