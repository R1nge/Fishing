using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OrderManager : GenericSingleton<OrderManager>
    {
        [SerializeField] private List<Order> orders;

        public void Add(Order order, int index)
        {
            if (orders.Count <= index)
            {
                orders.Add(order);
            }
            else
            {
                orders[index] = order;
            }
        }

        public void Remove(int index) => orders[index] = null;

        public List<Order> GetOrders => orders;
    }
}