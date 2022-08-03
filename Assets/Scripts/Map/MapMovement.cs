using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    public class MapMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 1.5f;
        private Vector3 _clickPosition;
        private Vector2 _dir;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private bool _canMove;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = target.GetComponent<Rigidbody2D>();
        }

        private void Start() => _clickPosition = transform.position;

        private void Update()
        {
            LookAt();
            HandleInput();
        }

        private void FixedUpdate() => Move();

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
            {
                if (!EventSystem.current.IsPointerOverGameObject() && !IsPointerOverUIObject())
                {
                    _clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                    _canMove = true;
                }
            }
        }

        private void Move()
        {
            if (!_canMove) return;

            if (target.transform.position == _clickPosition)
            {
                _canMove = false;
            }

            _dir = Vector2.MoveTowards(_rigidbody.position, _clickPosition, speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_dir);
        }

        private void LookAt()
        {
            Vector3 diff = _camera.ScreenToWorldPoint(Input.mousePosition) - target.transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            target.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }

        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
                {position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)};
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}