using UnityEngine;

namespace Map
{
    public class MapMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 1.5f;
        private Vector2 _targetPos;
        private Vector2 _dir;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private bool _canMove;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = target.GetComponent<Rigidbody2D>();
        }

        private void Start() => _targetPos = transform.position;

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _canMove = true;
            }

            if (_canMove)
            {
                _dir = Vector2.MoveTowards(_rigidbody.position, _targetPos, speed * Time.fixedDeltaTime);
                _rigidbody.MovePosition(_dir);
            }
        }
    }
}