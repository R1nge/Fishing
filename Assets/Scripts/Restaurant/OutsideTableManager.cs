using System.Collections.Generic;
using UnityEngine;

namespace Restaurant
{
    public class OutsideTableManager : MonoBehaviour
    {
        [SerializeField] private List<OutsideTable> outsideTables;

        private void Start() => Init();

        private void Init()
        {
            for (int i = 0; i < outsideTables.Count; i++)
            {
                outsideTables[i].SetIndex(i);
                outsideTables[i].SetSprite(null);
                outsideTables[i].SetOrder(null);
            }

            for (int i = 0; i < OrderManager.Instance.GetCompletedOrders.Count; i++)
            {
                outsideTables[i].SetSprite(OrderManager.Instance.GetCompletedOrders[i].GetCustomer());
                outsideTables[i].SetOrder(OrderManager.Instance.GetCompletedOrders[i]);
            }
        }
    }
}