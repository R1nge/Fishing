using System;
using UnityEngine;

namespace Other
{
    public class SwipeController : MonoBehaviour
    {
        [SerializeField] private bool detectSwipeOnlyAfterRelease;
        private const float SWIPE_THRESHOLD = 20f;
        private Vector2 _fingerDown;
        private Vector2 _fingerUp;
        private bool _canSwipe = true;

        public event Action OnSwipeUpEvent;
        public event Action OnSwipeDownEvent;
        public event Action OnSwipeLeftEvent;
        public event Action OnSwipeRightEvent;

        private void Awake() => DontDestroyOnLoad(gameObject);

        //TODO: Think of a better name
        public void SetCanSwipe(bool value) => _canSwipe = value;

        private void Update()
        {
            if (!_canSwipe) return;
            DesktopInput();
            MobileInput();
        }

        private void DesktopInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _fingerUp = Input.mousePosition;
                _fingerDown = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _fingerDown = Input.mousePosition;
                CheckSwipe();
            }
        }

        private void MobileInput()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerUp = touch.position;
                    _fingerDown = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        _fingerDown = touch.position;
                        CheckSwipe();
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _fingerDown = touch.position;
                    CheckSwipe();
                }
            }
        }

        private void CheckSwipe()
        {
            if (VerticalDir() > SWIPE_THRESHOLD && VerticalDir() > HorizontalDir())
            {
                if (_fingerDown.y - _fingerUp.y > 0)
                {
                    OnSwipeUp();
                }
                else if (_fingerDown.y - _fingerUp.y < 0)
                {
                    OnSwipeDown();
                }

                _fingerUp = _fingerDown;
            }
            else if (HorizontalDir() > SWIPE_THRESHOLD && HorizontalDir() > VerticalDir())
            {
                if (_fingerDown.x - _fingerUp.x > 0)
                {
                    OnSwipeRight();
                }
                else if (_fingerDown.x - _fingerUp.x < 0)
                {
                    OnSwipeLeft();
                }

                _fingerUp = _fingerDown;
            }
        }

        private float VerticalDir() => Mathf.Abs(_fingerDown.y - _fingerUp.y);

        private float HorizontalDir() => Mathf.Abs(_fingerDown.x - _fingerUp.x);

        private void OnSwipeUp() => OnSwipeUpEvent?.Invoke();

        private void OnSwipeDown() => OnSwipeDownEvent?.Invoke();

        private void OnSwipeLeft() => OnSwipeLeftEvent?.Invoke();

        private void OnSwipeRight() => OnSwipeRightEvent?.Invoke();
    }
}