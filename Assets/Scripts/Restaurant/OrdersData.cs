using System.Collections.Generic;

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
    }
}