using UnityEngine;
using UnityEngine.EventSystems;

namespace Restaurant
{
    public class Dish : MonoBehaviour, IDragHandler
    {
        private DishSo _dish;
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        public DishSo GetDish() => _dish;

        public void SetDish(DishSo value) => _dish = value;

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = new Vector3(
                _camera.ScreenToWorldPoint(eventData.position).x,
                _camera.ScreenToWorldPoint(eventData.position).y,
                0);
        }
    }
}