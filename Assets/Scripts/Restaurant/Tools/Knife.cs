using Restaurant.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Restaurant.Tools
{
    public class Knife : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPosition;
        private CutTableUI _ui;
        private FishStatus _fishStatus;

        private void Awake()
        {
            _startPosition = transform.position;
            _ui = FindObjectOfType<CutTableUI>();
            _fishStatus = FindObjectOfType<FishStatus>();
        }

        public void OnDrag(PointerEventData eventData) => transform.position = eventData.position;

        public void OnEndDrag(PointerEventData eventData) => transform.position = _startPosition;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Fish"))
            {
                transform.position = _startPosition;
                _fishStatus.Chop();
                _ui.Close();
            }
        }
    }
}