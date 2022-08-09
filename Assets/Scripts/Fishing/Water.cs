using UnityEngine;

namespace Fishing
{
    public class Water : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Rigidbody2D rb))
            {
                rb.drag = 2f;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Rigidbody2D rb))
            {
                rb.drag = 1f;
            }
        }
    }
}