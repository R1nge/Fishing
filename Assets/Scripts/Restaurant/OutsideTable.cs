using UnityEngine;

namespace Restaurant
{
    public class OutsideTable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer customer;
        [SerializeField] private SpriteRenderer dish;
        private MoneyManager _moneyManager;
        private int _index;
        private Order _order;
        private OrderManager _orderManager;

        private void Awake()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
            _orderManager = FindObjectOfType<OrderManager>();
        }

        public void SetIndex(int value) => _index = value;

        public void SetCustomer(Sprite sprite) => customer.sprite = sprite;

        public void SetDish(Sprite sprite) => dish.sprite = sprite;

        public void SetOrder(Order order) => _order = order;

        private void OnMouseDown()
        {
            var completeOrders = _orderManager.GetCompletedOrders;
            if (completeOrders.Count <= _index) return;
            if (completeOrders[_index].GetStatus())
            {
                _moneyManager.Earn(_order.GetOrder().dish.price);
                SetCustomer(null);
                SetDish(null);
                SetOrder(null);
                _orderManager.Clean(_index);
            }
        }
    }
}