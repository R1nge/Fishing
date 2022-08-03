using UnityEngine;

namespace Fishing
{
    public class BoatMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float minX, maxX;
        private readonly float _center = Screen.width / 2f;

        private void Update() => HandleInput();

        private void HandleInput()
        {
            if (Input.touchCount > 0)
            {
                Move(Input.GetTouch(0).position);
            }

            if (Input.GetMouseButton(0))
            {
                Move(Input.mousePosition);
            }
        }

        private void Move(Vector2 position)
        {
            if (position.x < _center && transform.position.x > minX)
            {
                transform.Translate(new Vector3(-speed, 0) * Time.deltaTime);
            }
            else if (position.x > _center && transform.position.x < maxX)
            {
                transform.Translate(new Vector3(speed, 0) * Time.deltaTime);
            }
        }
    }
}