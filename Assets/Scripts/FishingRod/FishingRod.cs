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
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }

        private void Start() => _startPosition = transform.position;

        private void Update()
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
                _rigidbody2D.MovePosition(_rigidbody2D.position - new Vector2(0, fishingRodSo.data.verticalSpeed * Time.deltaTime));
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
                var pos = transform.position;
                pos.y = -fishingRodSo.data.maxLength;
                transform.position = pos;
                _canMove = true;
                _state = States.Up;
            }
        }

        private void MovePc()
        {
            if (!Input.GetMouseButton(0)) return;
            Move(Input.mousePosition);
        }

        private void MoveMobile()
        {
            if (Input.touchCount <= 0) return;
            Move(Input.GetTouch(0).position);
        }

        private void Move(Vector3 target)
        {
            if (!_canMove) return;
            if (transform.position.y < _startPosition.y)
            {
                var position = new Vector2(_mainCamera.ScreenToWorldPoint(target).x, 0);
                _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(0, fishingRodSo.data.verticalSpeed * Time.deltaTime));
                _rigidbody2D.AddForce(position * (fishingRodSo.data.horizontalSpeed * Time.deltaTime));
            }
            else
            {
                transform.position = _startPosition;
                _canMove = false;
                _state = States.Idle;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_canMove && !_hasReachedBottom) return;
            if (!other.TryGetComponent(out FishMovementController movementController)) return;
            movementController.enabled = false;
            other.transform.parent = transform;
            other.transform.localPosition = Vector3.zero;
        }
    }
}