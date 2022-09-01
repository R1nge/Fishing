using UnityEngine;

namespace Restaurant
{
    public class OutsideTable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private MoneyManager _moneyManager;
        private int _index;
        private Order _order;

        private void Awake() => _moneyManager = FindObjectOfType<MoneyManager>();

        public void SetIndex(int value) => _index = value;

        public void SetSprite(Sprite sprite) => spriteRenderer.sprite = sprite;

        public void SetOrder(Order order) => _order = order;


        private void OnMouseDown()
        {
            if (OrderManager.Instance.GetCompletedOrders[_index].GetStatus())
            {
                _moneyManager.Earn(_order.GetOrder().dish.price);
                SetSprite(null);
                SetOrder(null);
                OrderManager.Instance.Clean(_index);
            }
        }
    }
}