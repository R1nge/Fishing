﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OrdersData : GenericSingleton<OrdersData>
    {
        public List<Order> orders = new List<Order>(6);
        public List<Order> completedOrder;

        public override void Awake()
        {
            base.Awake();
            for (int i = 0; i < orders.Capacity; i++)
            {
                orders.Add(new Order());
            }
        }

        public void Add(Order order, int index)
        {
            if (Instance.orders.Count <= index)
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
                TablesData.FreeAmount--;
                TablesData.Update();
                return;
            }

            var hasEmpty = false;
            var index = 0;
            for (; index < completedOrder.Count; index++)
            {
                if (completedOrder[index].GetDish() == null)
                {
                    hasEmpty = true;
                    break;
                }
            }

            if (!hasEmpty)
            {
                completedOrder.Add(order);
            }
            else
            {
                completedOrder[index] = order;
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
            completedOrder[index] = null;
            TablesData.FreeAmount++;
            TablesData.Update();
        }

        public List<Order> GetOrdersData => orders;

        public List<Order> GetCompletedOrders => completedOrder;
    }
}