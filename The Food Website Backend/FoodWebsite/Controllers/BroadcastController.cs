using FoodWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class BroadcastController : ApiController
    {
        [HttpGet]
        public void Add(Guid userID, Guid restaurantID, DateTime deadline)
        {
            Broadcast broadcast = new Broadcast
            {

                RestaurantID = restaurantID,
                UserID = userID,
                Deadline = deadline,
                BroadcastID = Guid.NewGuid()

            };

            Broadcast.Add(broadcast);
        }
        [HttpGet]
        public void Close(Guid id)
        {
            Broadcast.Close(id);
        }
        [HttpGet]
        public List<Broadcast> History()
        {
            return Broadcast.GetAll().Where(e => e.Active == false).ToList();
        }
        [HttpGet]
        public List<Order> Reciept(Guid id)
        {
            List<Guid> orderGuids = Broadcast.Get(id).Orders;
            List<Order> orders = Order.GetOrders(orderGuids);
            return orders;
        }
    }
}
