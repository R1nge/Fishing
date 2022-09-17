using System.Collections.Generic;

namespace Restaurant
{
    public class OrdersData : GenericSingleton<OrdersData>
    {
        public List<Order> orders;
        public List<Order> completedOrder;
    }
}