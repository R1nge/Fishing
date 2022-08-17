using UnityEngine;

namespace Restaurant
{
    public class Dish : MonoBehaviour
    {
        [SerializeField] private CookingRecipeSo recipe;
        private Vector3 _startPosition;
        private Camera _camera;

        private void Awake()
        {
            _startPosition = transform.position;
            _camera = Camera.main;
        }

        private void OnMouseDrag()
        {
            transform.position = _camera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y,
                    10)
            );
        }

        private void OnMouseUp() => transform.position = _startPosition;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent(out Table table))
            {
                if (table.Current == recipe)
                {
                    table.CompleteOrder();
                    Destroy(gameObject);
                }
            }
        }
    }
}