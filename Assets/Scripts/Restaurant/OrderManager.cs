using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OrderManager : GenericSingleton<OrderManager>
    {
        public void Add(Order order, int index)
        {
            if (OrdersData.Instance.orders.Count <= index)
            {
                OrdersData.Instance.orders.Add(order);
            }
            else
            {
                OrdersData.Instance.orders[index] = order;
            }
        }

        public void Remove(int index)
        {
            if (OrdersData.Instance.orders.Count > index)
                OrdersData.Instance.orders[index] = null;
        }

        public void Complete(Order order)
        {
            if (OrdersData.Instance.completedOrder.Count <= 0)
            {
                OrdersData.Instance.completedOrder.Add(order);
                EatTime(order);
                TablesData.FreeAmount--;
                TablesData.Update();
                return;
            }

            var hasEmpty = false;
            var index = 0;
            for (; index < OrdersData.Instance.completedOrder.Count; index++)
            {
                if (OrdersData.Instance.completedOrder[index].GetDish() == null)
                {
                    hasEmpty = true;
                    break;
                }
            }

            if (!hasEmpty)
            {
                OrdersData.Instance.completedOrder.Add(order);
            }
            else
            {
                OrdersData.Instance.completedOrder[index] = order;
            }

            EatTime(order);
            TablesData.FreeAmount--;
            TablesData.Update();
        }

        private void EatTime(Order order) => StartCoroutine(EatTime_c(order));

        private IEnumerator EatTime_c(Order order)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            order.SetStatus(true);
        }

        public void Clean(int index)
        {
            OrdersData.Instance.completedOrder[index] = null;
            TablesData.FreeAmount++;
            TablesData.Update();
        }

        public List<Order> GetOrdersData => OrdersData.Instance.orders;

        public List<Order> GetCompletedOrders => OrdersData.Instance.completedOrder;
    }
}