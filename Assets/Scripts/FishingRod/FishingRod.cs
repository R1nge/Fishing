using Other;
using UnityEngine;
using Zenject;

namespace FishingRod
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodSo fishingRodSo;
        private bool _canThrow = true;
        private bool _isPressed;
        private bool _canCatch;
        private int _distance;
        private Vector3 _start;
        private Rigidbody2D _rigidbody2D;
        private DistanceJoint2D _joint;
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
            _joint = GetComponent<DistanceJoint2D>();
            _collision = GetComponent<HookCollision>();
        }

        private void FixedUpdate() => Pull();

        private void SetCanSwipe() => _swipeController.SetCanSwipe(_canThrow);

        private void Throw()
        {
            if (!_canThrow) return;
            _joint.distance = fishingRodSo.data.maxLength;
            _rigidbody2D.AddForce(Vector2.down * fishingRodSo.data.horizontalSpeed);
            _canCatch = true;
            _canThrow = false;
            _isPressed = false;

            SetCanSwipe();
        }

        private void Pull()
        {
            if (_canThrow || !_isPressed) return;

            var distance = Vector2.Distance(_start, transform.position);
            distance -= fishingRodSo.data.verticalSpeed;
            _joint.distance = distance;

            if (_joint.distance <= 0.31f)
            {
                _canCatch = false;
                _canThrow = true;
                SetCanSwipe();
                DestroyChildren();
            }

            if (transform.localPosition.x < 0 && _rigidbody2D.velocity != Vector2.zero)
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_canCatch) return;

            if (other.TryGetComponent(out Fish.Fish fish))
            {
                _collision.AddToInventory(fish.ingredient);
                _canCatch = false;
            }

            _collision.Attach(other);
        }

        private void DestroyChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
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