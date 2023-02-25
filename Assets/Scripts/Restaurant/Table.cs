using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private int minTolerance, maxTolerance;
        [SerializeField] private int minWaitTime, maxWaitTime;
        [SerializeField] private CustomerSprites customers;
        private CookingRecipeSo _current;
        private Recipes _recipes;
        private int _tolerance;
        private int _index;
        private OrdersData _ordersData;

        public event Action<Sprite> OnOrderChangedEvent;
        public event Action<Sprite> OnCustomerChangedEvent;
        public event Action<int> OnToleranceChangedEvent;

        private void Awake()
        {
            _recipes = FindObjectOfType<Recipes>();
            _ordersData = GameObject.FindWithTag("GameManager").GetComponentInChildren<OrdersData>();
        }

        private void Start()
        {
            Init(_index);
            InvokeRepeating("DecreaseTolerance", 1, 1);
        }

        public void SetIndex(int index) => _index = index;

        private void Init(int index)
        {
            var orders = _ordersData.GetOrdersData;
            if (orders.Count > index && orders[index] != null && orders[index].GetRecipe() != null)
            {
                _current = orders[index].GetRecipe();
                _tolerance = orders[index].GetTolerance();
                OnOrderChanged(orders[index].GetCustomer(), _current.dish.sprite, _tolerance);
            }
            else
            {
                StartCoroutine(Pick_c());
            }
        }

        private void DecreaseTolerance()
        {
            if (!HasOrder(_index)) return;
            _tolerance -= 1;
            OnToleranceChangedEvent?.Invoke(_tolerance);
            _ordersData.GetOrdersData[_index].SetTolerance(_tolerance);
            if (_tolerance <= 0)
            {
                StartCoroutine(Pick_c());
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!HasOrder(_index)) return;
            if (other.TryGetComponent(out Dish dish))
            {
                if (dish.GetDish().title == _current.dish.title)
                {
                    //BUG: Can complete 2 orders simultaneously with one dish
                    Destroy(dish.gameObject);
                    CompleteOrder();
                }
            }
        }

        private void CompleteOrder()
        {
            _ordersData.Complete(_ordersData.GetOrdersData[_index]);
            StartCoroutine(Pick_c());
        }

        private IEnumerator Pick_c()
        {
            _ordersData.Remove(_index);
            OnOrderChanged(null, null, 0);
            _current = null;
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            Pick();
        }

        private void Pick()
        {
            _current = _recipes.Pick();
            PickTolerance();
            CreateOrder();
        }

        private void CreateOrder()
        {
            var order = new Order();
            order.SetSprite(customers.GetRandom());
            order.SetDish(_current.prefab.GetComponent<SpriteRenderer>().sprite);
            order.SetStatus(false);
            order.SetTolerance(_tolerance);
            order.SetRecipe(_current);
            _ordersData.Add(order, _index);
            OnOrderChanged(order.GetCustomer(), order.GetDish(), order.GetTolerance());
        }

        private void OnOrderChanged(Sprite customer, Sprite dish, int tolerance)
        {
            OnCustomerChangedEvent?.Invoke(customer);
            OnOrderChangedEvent?.Invoke(dish);
            OnToleranceChangedEvent?.Invoke(tolerance);
        }

        private bool HasOrder(int index)
        {
            var order = _ordersData.GetOrdersData[index];
            return order != null && order.GetRecipe() != null;
        }

        private void PickTolerance() => _tolerance = Random.Range(minTolerance, maxTolerance);
    }
}