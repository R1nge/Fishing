using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OutsideTableManager : MonoBehaviour
    {
        [SerializeField] private List<OutsideTable> outsideTables;
        [SerializeField] private Sprite moneySprite;

        private void Start()
        {
            for (int i = 0; i < OrdersData.Instance.GetCompletedOrders.Count; i++)
            {
                OrdersData.Instance.GetCompletedOrders[i].OnStatusChanged += Init;
            }

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < outsideTables.Count; i++)
            {
                outsideTables[i].SetIndex(i);
                outsideTables[i].SetCustomer(null);
                outsideTables[i].SetOrder(null);
                outsideTables[i].SetDish(null);
            }

            var completeOrders = OrdersData.Instance.GetCompletedOrders;
            for (int i = 0; i < completeOrders.Count; i++)
            {
                if (completeOrders[i] == null) continue;
                outsideTables[i].SetCustomer(completeOrders[i].GetCustomer());
                outsideTables[i].SetOrder(completeOrders[i]);
                outsideTables[i].SetDish(completeOrders[i].GetStatus() ? moneySprite : completeOrders[i].GetDish());
            }
        }

        private void OnDestroy()
        {
            if (OrdersData.Instance == null) return;
            for (int i = 0; i < OrdersData.Instance.GetCompletedOrders.Count; i++)
            {
                OrdersData.Instance.GetCompletedOrders[i].OnStatusChanged -= Init;
            }
        }
    }
}