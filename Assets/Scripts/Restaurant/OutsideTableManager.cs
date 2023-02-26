using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Restaurant
{
    public class OutsideTableManager : MonoBehaviour
    {
        [SerializeField] private List<OutsideTable> outsideTables;
        [SerializeField] private Sprite moneySprite;
        private OrdersData _ordersData;

        [Inject]
        public void Constructor(OrdersData ordersData) => _ordersData = ordersData;

        private void Start()
        {
            for (int i = 0; i < _ordersData.GetCompletedOrders.Count; i++)
            {
                _ordersData.GetCompletedOrders[i].OnStatusChanged += Init;
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

            var completeOrders = _ordersData.GetCompletedOrders;
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
            if (_ordersData == null) return;
            for (int i = 0; i < _ordersData.GetCompletedOrders.Count; i++)
            {
                _ordersData.GetCompletedOrders[i].OnStatusChanged -= Init;
            }
        }
    }
}