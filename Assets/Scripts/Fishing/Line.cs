using UnityEngine;

namespace Fishing
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private Vector3 rodOffset, hookOffset;
        [SerializeField] private Transform rod, hook;
        private LineRenderer _lineRenderer;

        private void Awake() => _lineRenderer = GetComponent<LineRenderer>();

        private void LateUpdate()
        {
            _lineRenderer.SetPosition(0, rod.transform.position + rodOffset);
            _lineRenderer.SetPosition(1, hook.transform.position + hookOffset);
        }
    }
}