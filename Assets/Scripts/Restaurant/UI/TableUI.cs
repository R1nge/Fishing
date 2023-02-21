using TMPro;
using UnityEngine;

namespace Restaurant.UI
{
    public class TableUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer order;
        [SerializeField] private TextMeshProUGUI tolerance;
        [SerializeField] private SpriteRenderer customer;
        private Table _table;

        private void Awake()
        {
            _table = GetComponent<Table>();
            _table.OnCustomerChangedEvent += UpdateCustomer;
            _table.OnOrderChangedEvent += UpdateOrder;
            _table.OnToleranceChangedEvent += UpdateTolerance;
        }

        private void UpdateCustomer(Sprite sprite) => customer.sprite = sprite;

        private void UpdateTolerance(int value) => tolerance.SetText(value == 0 ? string.Empty : value.ToString());

        private void UpdateOrder(Sprite sprite) => order.sprite = sprite;

        private void OnDestroy()
        {
            _table.OnCustomerChangedEvent -= UpdateCustomer;
            _table.OnOrderChangedEvent -= UpdateOrder;
            _table.OnToleranceChangedEvent -= UpdateTolerance;
        }
    }
}