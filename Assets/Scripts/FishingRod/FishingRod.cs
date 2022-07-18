using System;
using Fish;
using UnityEngine;

namespace FishingRod
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodStats stats;
        [SerializeField] private Camera mainCamera;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveDown()
        {
            if (transform.position.y > -stats.maxLength)
            {
                _rigidbody2D.velocity = Vector2.down * stats.speed;
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
                _rigidbody2D.velocity = Vector2.up * stats.speed;
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
            position.x = mainCamera.ScreenToWorldPoint(target).x;
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * stats.speed);
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