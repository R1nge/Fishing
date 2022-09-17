using System.Collections;
using Restaurant.UI;
using UnityEngine;

namespace Restaurant
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private int minTolerance, maxTolerance;
        [SerializeField] private int minWaitTime, maxWaitTime;
        [SerializeField] private Sprite[] sprites;
        private OrderManager _orderManager;
        private CookingRecipeSo _current;
        private Recipes _recipes;
        private TableUI _ui;
        private int _tolerance;
        private bool _hasOrder;
        private int _index;

        private void Awake()
        {
            _orderManager = FindObjectOfType<OrderManager>();
            _recipes = FindObjectOfType<Recipes>();
            _ui = GetComponent<TableUI>();
        }

        private void Start()
        {
            Init(_index);
            InvokeRepeating("DecreaseTolerance", 1, 1);
        }

        public void SetIndex(int index) => _index = index;

        private void Init(int index)
        {
            var orders = _orderManager.GetOrdersData;
            if (orders.Count > index && orders[index] != null && orders[index].GetOrder() != null)
            {
                _current = orders[index].GetOrder();
                _tolerance = orders[index].GetTolerance();
                _ui.UpdateOrder(_current.name);
                _ui.UpdateTolerance(_tolerance.ToString());
                _ui.UpdateSprite(orders[index].GetCustomer());
                _hasOrder = true;
            }
            else
            {
                StartCoroutine(Pick_c());
            }
        }

        private void DecreaseTolerance()
        {
            if (!_hasOrder) return;
            _tolerance -= 1;
            _ui.UpdateTolerance(_tolerance.ToString());
            _orderManager.GetOrdersData[_index].SetTolerance(_tolerance);
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
            _orderManager.Complete(_orderManager.GetOrdersData[_index]);
            StartCoroutine(Pick_c());
        }

        private IEnumerator Pick_c()
        {
            _orderManager.Remove(_index);
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
            _ui.UpdateOrder(_current.name);
            _ui.UpdateTolerance(_tolerance.ToString());
            _ui.UpdateSprite(sprites[Random.Range(0, sprites.Length)]);
            var order = new Order();
            order.SetSprite(_ui.GetSprite());
            order.SetDish(_current.prefab.GetComponent<SpriteRenderer>().sprite);
            order.SetStatus(false);
            order.SetTolerance(_tolerance);
            order.SetOrder(_current);
            _orderManager.Add(order, _index);
            _hasOrder = true;
        }

        private void PickTolerance() => _tolerance = Random.Range(minTolerance, maxTolerance);
    }
}