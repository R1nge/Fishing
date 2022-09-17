using System;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OutsideTableManager : MonoBehaviour
    {
        [SerializeField] private List<OutsideTable> outsideTables;
        [SerializeField] private Sprite moneySprite;
        private OrderManager _orderManager;

        private void Awake()
        {
            _orderManager = FindObjectOfType<OrderManager>();

            for (int i = 0; i < _orderManager.GetCompletedOrders.Count; i++)
            {
                _orderManager.GetCompletedOrders[i].OnStatusChanged += Init;
            }
        }

        private void Start() => Init();

        private void Init()
        {
            for (int i = 0; i < outsideTables.Count; i++)
            {
                outsideTables[i].SetIndex(i);
                outsideTables[i].SetCustomer(null);
                outsideTables[i].SetOrder(null);
                outsideTables[i].SetDish(null);
            }

            var completeOrders = _orderManager.GetCompletedOrders;
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
            if (_orderManager == null) return;
            for (int i = 0; i < _orderManager.GetCompletedOrders.Count; i++)
            {
                _orderManager.GetCompletedOrders[i].OnStatusChanged -= Init;
            }
        }
    }
}