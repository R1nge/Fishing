using UnityEngine;

namespace FishingRod
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] private FishingRodSo fishingRodSo;
        private bool _canThrow;
        private bool _isPressed;
        private bool _canCatch;
        private int _distance;
        private Rigidbody2D _rigidbody2D;
        private DistanceJoint2D _joint;
        private HookCollision _collision;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _joint = GetComponent<DistanceJoint2D>();
            _collision = GetComponent<HookCollision>();
        }

        private void FixedUpdate() => Pull();

        public void StartPulling() => _isPressed = true;

        public void StopPulling() => _isPressed = false;

        public void Throw()
        {
            if (!_canThrow) return;
            _joint.distance = fishingRodSo.data.maxLength;
            _rigidbody2D.AddForce(Vector2.right * fishingRodSo.data.horizontalSpeed);
            _canCatch = true;
            _canThrow = false;
        }

        private void Pull()
        {
            if (_canThrow || !_isPressed) return;
            _joint.distance -= fishingRodSo.data.verticalSpeed;
            if (_joint.distance <= 0.31f)
            {
                _canCatch = false;
                _canThrow = true;
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

            if (other.TryGetComponent(out Fish.Fish myfish))
            {
                _collision.AddToInventory(myfish.fishSo);
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
    }
}