using UnityEngine;

namespace Restaurant
{
    public class OutsideTable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer customer;
        [SerializeField] private SpriteRenderer dish;
        private MoneyManager _moneyManager;
        private Order _order;
        private int _index;

        private void Awake() => _moneyManager = FindObjectOfType<MoneyManager>();

        public void SetIndex(int value) => _index = value;

        public void SetCustomer(Sprite sprite) => customer.sprite = sprite;

        public void SetDish(Sprite sprite) => dish.sprite = sprite;

        public void SetOrder(Order order) => _order = order;

        private void OnMouseDown()
        {
            var completeOrders = OrdersData.Instance.GetCompletedOrders;
            if (completeOrders.Count <= _index) return;
            if (completeOrders[_index] == null) return;
            if (completeOrders[_index].GetStatus())
            {
                _moneyManager.Earn(_order.GetRecipe().dish.price);
                SetCustomer(null);
                SetDish(null);
                SetOrder(null);
                OrdersData.Instance.Clean(_index);
            }
        }
    }
}