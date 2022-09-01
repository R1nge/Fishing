using System.Collections;
using System.Globalization;
using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private int minTolerance, maxTolerance;
        [SerializeField] private int minWaitTime, maxWaitTime;
        [SerializeField] private Sprite[] sprites;
        private CookingRecipeSo _current;
        private MoneyManager _moneyManager;
        private Recipes _recipes;
        private TableUI _ui;
        private int _tolerance;
        private bool _hasOrder;
        private int _index;

        //OrderManager.Instance;

        private void Awake()
        {
            _moneyManager = FindObjectOfType<MoneyManager>();
            _recipes = FindObjectOfType<Recipes>();
            _ui = GetComponent<TableUI>();
        }

        private void Start() => InvokeRepeating("DecreaseTolerance", 1, 1);

        public void Init(int index)
        {
            _index = index;
            var orders = OrderManager.Instance.GetOrders;
            if (orders.Count > index && orders[index].GetOrder() != null)
            {
                _current = orders[index].GetOrder();
                _tolerance = orders[index].GetTolerance();
                UpdateUI();
                _ui.UpdateSprite(orders[index].GetCustomer());
                _hasOrder = true;
            }
            else
            {
                Pick();
            }

            _ui.UpdateTolerance(_tolerance.ToString(CultureInfo.InvariantCulture));
        }

        private void DecreaseTolerance()
        {
            if (!_hasOrder) return;
            _tolerance -= 1;
            _ui.UpdateTolerance(_tolerance.ToString());
            OrderManager.Instance.GetOrders[_index].SetTolerance(_tolerance);
            if (_tolerance <= 0)
            {
                StartCoroutine(Pick_c());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_hasOrder) return;
            if (other.TryGetComponent(out Dish dish))
            {
                if (dish.GetDish().title == _current.dish.title)
                {
                    CompleteOrder();
                    Destroy(dish.gameObject);
                }
            }
        }

        private void CompleteOrder()
        {
            _moneyManager.Earn(_current.dish.price);
            StartCoroutine(Pick_c());
        }

        private IEnumerator Pick_c()
        {
            OrderManager.Instance.Remove(_index);
            _ui.UpdateOrder(null);
            _ui.UpdateTolerance(null);
            _ui.UpdateSprite(null);
            _hasOrder = false;
            _current = null;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            Pick();
        }

        private void Pick()
        {
            _current = _recipes.Pick();
            PickTolerance();
            UpdateUI();
            _ui.UpdateSprite(sprites[Random.Range(0, sprites.Length)]);
            var order = new Order();
            order.SetSprite(_ui.GetSprite());
            order.SetDish(_current.prefab.GetComponent<SpriteRenderer>().sprite);
            order.SetStatus(false);
            order.SetTolerance(_tolerance);
            order.SetOrder(_current);
            OrderManager.Instance.Add(order, _index);
            _hasOrder = true;
            Init(_index);
        }

        private void UpdateUI()
        {
            _ui.UpdateOrder(_current.name);
            _ui.UpdateTolerance(_tolerance.ToString());
        }

        private void PickTolerance() => _tolerance = Random.Range(minTolerance, maxTolerance);
    }
}