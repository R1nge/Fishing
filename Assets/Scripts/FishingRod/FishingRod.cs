using Fish;
using UnityEngine;

namespace FishingRod
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodSo fishingRodSo;
        private Camera _mainCamera;
        private bool _hasReachedBottom;
        private bool _canMove;
        private Vector3 _startPosition;
        private DepthUI _depthUI;
        private Rigidbody2D _rigidbody2D;

        private enum States
        {
            Idle,
            Down,
            Up
        }

        private States _state = States.Idle;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _depthUI = FindObjectOfType<DepthUI>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            Application.targetFrameRate = 9999;
        }

        private void Start() => _startPosition = transform.position;

        private void FixedUpdate()
        {
            _depthUI.UpdateUI(Vector3.Distance(transform.position, _startPosition).ToString("F2") + " m");
            switch (_state)
            {
                case States.Idle:
                    if (Input.GetMouseButtonDown(0)) _state = States.Down;
                    break;
                case States.Down:
                    MoveDown();
                    break;
                case States.Up:
                    MoveUp();

#if UNITY_ANDROID && !UNITY_EDITOR
                    MoveMobile();
#else
                    MovePc();
#endif
                    break;
            }
        }

        private void MoveDown()
        {
            if (transform.position.y > -fishingRodSo.data.maxLength)
            {
                _rigidbody2D.velocity = Vector2.down * fishingRodSo.data.speed;
            }
            else
            {
                _state = States.Up;
                _canMove = true;
            }
        }

        private void MoveUp()
        {
            if (transform.position.y < _startPosition.y)
            {
                _rigidbody2D.velocity = Vector2.up * fishingRodSo.data.speed;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
                transform.position = _startPosition;
                _canMove = false;
                _state = States.Idle;
            }
        }

        private void MovePc() => Move(Input.mousePosition);

        private void MoveMobile()
        {
            if (Input.touchCount <= 0) return;
            Move(Input.GetTouch(0).position);
        }

        private void Move(Vector3 target)
        {
            if (!_canMove) return;
            var position = transform.position;
            position.x = _mainCamera.ScreenToWorldPoint(target).x;
            transform.position =
                Vector3.MoveTowards(transform.position, position, Time.deltaTime * fishingRodSo.data.speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_canMove && !_hasReachedBottom) return;

            if (other.TryGetComponent(out FishMovementController movementController))
            {
                movementController.enabled = false;
            }

            other.transform.parent = transform;
            other.transform.localPosition = Vector3.zero;
        }
    }
}