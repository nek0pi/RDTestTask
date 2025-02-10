using UnityEngine;

namespace Gameplay.Environment
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private void LateUpdate()
        {
            Vector3 targetPositionOffset = _target.position + _offset;
            var targetPositionOffsetY = targetPositionOffset.y;
            var desiredPosition = new Vector3(_camera.transform.position.x, targetPositionOffsetY,
                _camera.transform.position.z);
            transform.position = desiredPosition;
        }
    }
}