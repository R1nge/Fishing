using UnityEngine;
using UnityEngine.EventSystems;

namespace Restaurant.Tools
{
    public abstract class Tool : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        protected FishStatus FishStatus;
        private Vector3 _startPosition;

        protected virtual void Awake()
        {
            _startPosition = transform.position;
            FishStatus = FindObjectOfType<FishStatus>();
        }

        public void OnDrag(PointerEventData eventData) => transform.position = eventData.position;

        public void OnEndDrag(PointerEventData eventData) => transform.position = _startPosition;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Fish"))
            {
                transform.position = _startPosition;
                Action();
            }
        }

        protected abstract void Action();
    }
}