using System;
using System.Diagnostics;
using Other;
using UnityEngine;
using Zenject;

namespace FishingRod
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodSo fishingRodSo;
        private bool _isThrown;
        private bool _isGoingUp;
        private float _distance;
        private Vector3 _start;
        private Rigidbody2D _rigidbody2D;
        private HookCollision _collision;
        private SwipeController _swipeController;

        [Inject]
        public void Constructor(SwipeController swipeController)
        {
            _swipeController = swipeController;
            _swipeController.OnSwipeDownEvent += Throw;
        }

        private void Awake()
        {
            _start = transform.position;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collision = GetComponent<HookCollision>();
        }

        private void Update()
        {
            _distance = Vector2.Distance(_start, transform.position);

            if (_distance >= fishingRodSo.data.maxLength)
            {
                if (!_isGoingUp)
                {
                    _rigidbody2D.velocity = Vector2.zero;
                    _isGoingUp = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isGoingUp)
            {
                Pull();
                MovePc();
                MoveMobile();
            }
            else
            {
                if (_isThrown)
                {
                    _rigidbody2D.MovePosition(_rigidbody2D.position + Vector2.down * fishingRodSo.data.verticalSpeed);
                }
            }
        }

        private void Throw()
        {
            _isThrown = true;
            SetCanSwipe();
        }

        private void SetCanSwipe() => _swipeController.SetCanSwipe(!_isThrown);

        private void Pull()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + Vector2.up * fishingRodSo.data.verticalSpeed);
            _distance -= fishingRodSo.data.verticalSpeed;

            if (_distance <= 0.31f)
            {
                _isThrown = false;
                _isGoingUp = false;
                SetCanSwipe();
            }
        }

        [Conditional("PLATFORM_ANDROID")]
        private void MoveMobile()
        {
            if (!_isThrown)
            {
               //TODO: move with gyro
            }
        }

        [Conditional("PLATFORM_STANDALONE")]
        private void MovePc()
        {
            if (!_isThrown)
            {
                //TODO: move after cursor
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Fish.Fish fish))
            {
                _collision.AddToInventory(fish.ingredient);
                _isGoingUp = true;
            }

            _collision.Attach(other);
        }

        private void OnDestroy()
        {
            if (_swipeController)
            {
                _swipeController.OnSwipeDownEvent -= Throw;
            }
        }
    }
}