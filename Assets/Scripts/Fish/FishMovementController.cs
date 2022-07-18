using UnityEngine;

namespace Fish
{
    public class FishMovementController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxPositionX;
        private Vector3 _direction;

        private void Start()
        {
            var random = Random.Range(0, 2);
            _direction = random switch
            {
                0 => Vector3.right,
                1 => Vector3.left,
                _ => _direction
            };
        }

        private void Update()
        {
            if (transform.position.x <= -maxPositionX)
                _direction = Vector3.right;
            else if (transform.position.x >= maxPositionX)
                _direction = Vector3.left;

            transform.Translate(_direction * (speed * Time.deltaTime));
        }
    }
}