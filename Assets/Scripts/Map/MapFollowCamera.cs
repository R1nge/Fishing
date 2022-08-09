using UnityEngine;

namespace Map
{
    public class MapFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Camera _camera;
        private Vector3 _position;

        private void Awake() => _camera = Camera.main;

        private void LateUpdate() =>
            _camera.transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}