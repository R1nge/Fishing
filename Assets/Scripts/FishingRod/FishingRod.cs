using System;
using Fishing;
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
        private Rigidbody2D _rigidbody2D;
        private HookCollision _collision;
        private int _distance;
        private BoatMovement _boatMovement;
        private readonly float _center = Screen.width / 2f;

        private enum States
        {
            Idle,
            Down,
            Up
        }

        [SerializeReference] private States _state = States.Idle;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.WakeUp();
            _collision = GetComponent<HookCollision>();
            _boatMovement = FindObjectOfType<BoatMovement>(); //TODO: Make Input manager that handles input data
        }

        private void Start()
        {
            _startPosition = Vector3.zero;
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
            _boatMovement.enabled = false;
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
                var touchPos = new Vector2(target.x, 0);
                if (touchPos.x < _center)
                {
                    _rigidbody2D.AddForce(new Vector2(-fishingRodSo.data.horizontalSpeed * Time.fixedDeltaTime, 0));
                }
                else if (touchPos.x > _center)
                {
                    _rigidbody2D.AddForce(new Vector2(fishingRodSo.data.horizontalSpeed * Time.fixedDeltaTime, 0));
                }

                var dir = new Vector2(0, fishingRodSo.data.verticalSpeed * Time.fixedDeltaTime);
                _rigidbody2D.MovePosition(_rigidbody2D.position + dir);
            }
            else
            {
                _rigidbody2D.Sleep();
                _rigidbody2D.velocity = Vector2.zero;
                transform.localPosition = _startPosition;
                _distance = 0;
                _canMove = false;
                DestroyChildren();
                _boatMovement.enabled = true;
                _state = States.Idle;
            }
        }

        private void DestroyChildren()
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                var child = transform.GetChild(i).gameObject;
                Destroy(child);
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
        }
    }
}