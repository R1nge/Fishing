using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OrderManager : GenericSingleton<OrderManager>
    {
        [SerializeField] private List<Order> orders;
        [SerializeField] private List<Order> completedOrder;

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

        public void Remove(int index)
        {
            if (orders.Count > index)
                orders[index] = null;
        }

        public void Complete(Order order)
        {
            if (completedOrder.Count <= 0)
            {
                completedOrder.Add(order);
                EatTime(order);
                return;
            }
            //TODO: FIX FIX FIX FIX
            for (int i = 0; i < completedOrder.Count; i++)
            {
                if (completedOrder[i].GetDish() == null)
                {
                    completedOrder[i] = order;
                }
                else
                {
                    completedOrder.Add(order);
                }
            }

            EatTime(order);
        }

        private void EatTime(Order order) => StartCoroutine(EatTime_c(order));

        private IEnumerator EatTime_c(Order order)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            order.SetStatus(true);
        }

        public void Clean(int index) => completedOrder[index] = null;

        public List<Order> GetOrders => orders;

        public List<Order> GetCompletedOrders => completedOrder;
    }
}