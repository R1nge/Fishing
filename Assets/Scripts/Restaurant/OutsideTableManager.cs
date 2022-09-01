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
            for (int i = 0; i < OrderManager.Instance.GetOrders.Count; i++)
            {
                outsideTables[i].SetSprite(OrderManager.Instance.GetOrders[i].GetCustomer());
            }
        }
    }
}