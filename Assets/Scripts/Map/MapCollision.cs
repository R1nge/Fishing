using System.Collections;
using Other;
using UnityEngine;

namespace Map
{
    public class MapCollision : MonoBehaviour
    {
        private bool _canCollide;
        private MapMovement _movement;

        private void Awake() => _movement = FindObjectOfType<MapMovement>();

        private void Start() => StartCoroutine(EnableCollision());

        private void OnCollisionEnter2D(Collision2D other)
        {
            // if (!other.transform.TryGetComponent(out LoadScene location)) return;
            // if (!_canCollide) return;
            // _movement.enabled = false;
            // location.LoadWithString();
        }

        private IEnumerator EnableCollision()
        {
            yield return new WaitForSeconds(.5f);
            _canCollide = true;
        }
    }
}