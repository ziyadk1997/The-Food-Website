using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Order
    {
        private static Dictionary<Guid, Order> Orders { set; get; } = new Dictionary<Guid, Order>();

        public Guid OrderID { set; get; }
        public String Comments { set; get; }
        public Guid UserId { set; get; }

        public Dictionary<Guid, int> Items { set; get; }

        public static void Add(Order order)
        {
            Orders.Add(order.OrderID, order);
        }

        public static Order Get(Guid id)
        {
            if (Orders.ContainsKey(id))
            {
                return Orders[id];
            }
            else
            {
                return null;
            }
        }

        public static List<Order> GetOrders(List<Guid> orderGuids)
        {
            List<Order> orders = new List<Order>();
            foreach(var guid in orderGuids)
            {
                orders.Add(Orders[guid]);
            }
            return orders;
        }
    }
}
