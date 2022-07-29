using System;
using UI;
using UnityEngine;

namespace FishingRod
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodSo fishingRodSo;
        [SerializeField] private DepthUI depthUI; //Generates garbage only in Mono
        private bool _canMove;
        private Vector3 _startPosition;
        private Camera _mainCamera;
        private Rigidbody2D _rigidbody2D;
        private HookCollision _collision;
        private int _distance;

        private enum States
        {
            Idle,
            Down,
            Up
        }

        [SerializeReference] private States _state = States.Idle;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.WakeUp();
            _collision = GetComponent<HookCollision>();
        }

        private void Start()
        {
            _startPosition = _rigidbody2D.position;
            depthUI.UpdateUI(_distance);
        }

        private void FixedUpdate()
        {
            switch (_state)
            {
                case States.Idle:
                    gameObject.SetActive(false);
                    break;
                case States.Down:
                    UpdateUI();
                    MoveDown();
                    break;
                case States.Up:
                    UpdateUI();
#if UNITY_ANDROID && !UNITY_EDITOR
                    MoveMobile();
#else
                    MovePc();
#endif
                    break;
            }
        }

        public void SwitchState(string state)
        {
            if (Equals(_state, (States) Enum.Parse(typeof(States), "Up"))) return;
            _state = (States) Enum.Parse(typeof(States), state);
        }

        private void MoveDown()
        {
            if (_rigidbody2D.position.y + fishingRodSo.data.maxLength <= 0.05f)
            {
                var pos = _rigidbody2D.position;
                pos.y = -fishingRodSo.data.maxLength;
                _rigidbody2D.position = pos;
                _canMove = true;
                _state = States.Up;
            }
            else
            {
                var dir = new Vector2(0, fishingRodSo.data.verticalSpeed * Time.fixedDeltaTime);
                _rigidbody2D.MovePosition(_rigidbody2D.position - dir);
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
                var touchPos = new Vector2(_mainCamera.ScreenToWorldPoint(target).x, 0);
                var dir = new Vector2(0, fishingRodSo.data.verticalSpeed * Time.fixedDeltaTime);
                _rigidbody2D.MovePosition(_rigidbody2D.position + dir);
                _rigidbody2D.AddForce(touchPos * (fishingRodSo.data.horizontalSpeed * Time.fixedDeltaTime));
            }
            else
            {
                _rigidbody2D.Sleep();
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.position = _startPosition;
                _distance = 0;
                _canMove = false;
                _state = States.Idle;
            }
        }

        private void UpdateUI()
        {
            if (Math.Abs(_rigidbody2D.position.y - _distance) >= 1f)
            {
                _distance = (int) _rigidbody2D.position.y;
                depthUI.UpdateUI(-_distance);
            }
            else if (Math.Abs(_rigidbody2D.position.y + _distance) >= 1f)
            {
                _distance = (int) _rigidbody2D.position.y;
                depthUI.UpdateUI(-_distance);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_canMove) return;

            _collision.Attach(other);

            if (other.TryGetComponent(out Fish.Fish myfish))
            {
                _collision.AddToInventory(myfish.fishSo);
            }

            // if (other.TryGetComponent(out HingeJoint2D joint2D))
            // {
            //     joint2D.limits = new JointAngleLimits2D()
            //     {
            //         max = 320,
            //         min = 220
            //     };
            //     joint2D.connectedBody = _rigidbody2D;
            //     joint2D.useLimits = true;
            // }
        }
    }
}