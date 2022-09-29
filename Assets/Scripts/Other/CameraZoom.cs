using UnityEngine;

namespace Other
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float minScale, maxScale;
        [SerializeField] private float zoomModifierSpeed = 0.1f;
        private Camera _mainCamera;
        private float _touchesPrevPosDifference, _touchesCurPosDifference, _zoomModifier;
        private Vector2 _firstTouchPrevPos, _secondTouchPrevPos;

        private void Awake() => _mainCamera = Camera.main;

        private void LateUpdate()
        {
            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                _firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                _secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                _touchesPrevPosDifference = (_firstTouchPrevPos - _secondTouchPrevPos).magnitude;
                _touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                _zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

                if (_touchesPrevPosDifference > _touchesCurPosDifference)
                    _mainCamera.orthographicSize += _zoomModifier;
                if (_touchesPrevPosDifference < _touchesCurPosDifference)
                    _mainCamera.orthographicSize -= _zoomModifier;
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                _mainCamera.orthographicSize -= 2f;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                _mainCamera.orthographicSize += 2f;

            _mainCamera.orthographicSize = Mathf.Clamp(_mainCamera.orthographicSize, minScale, maxScale);
        }
    }
}