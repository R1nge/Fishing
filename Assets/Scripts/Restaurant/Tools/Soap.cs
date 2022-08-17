using Restaurant.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Restaurant.Tools
{
    public class Soap : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;
        private FishStatus _fishStatus;
        private WasherUI _ui;

        private void Awake()
        {
            _startPosition = transform.position;
            _fishStatus = FindObjectOfType<FishStatus>();
            _ui = FindObjectOfType<WasherUI>();
        }

        public void OnDrag(PointerEventData eventData) => transform.position = eventData.position;

        public void OnEndDrag(PointerEventData eventData) => transform.position = _startPosition;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Fish"))
            {
                transform.position = _startPosition;
                _fishStatus.Wash();
                _ui.Close();
            }
        }
    }
}