using FoodWebsite.DAL;
using FoodWebsite.UserIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class BroadcastController : ApiController
    {
        [HttpGet]
        public void Add(Guid restaurantID, DateTime deadline)
        {
            Restaurant restaurant = DAL.Restaurant.Get(restaurantID);

            Broadcast broadcast = new Broadcast
            {
                Restaurant = restaurant,
                Active = true,
                UserId = UserIdentityManager.GetUserId(),
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
        public void AddOrder(Order order,Guid id)
        {
            Broadcast.Get(id).Orders.Add(order);
        }
        [HttpGet]
        public List<Order> Reciept(Guid id)
        {
            return Broadcast.Get(id).Orders;
        }

        [HttpGet]
        public List<Broadcast> Active()
        {
            return Broadcast.GetAll().Where(e => e.Active == true).ToList();
        }
        [HttpGet]
        public static List<ItemValue> GetCurrentOrder(Guid id)
        {
            Guid UserID = UserIdentityManager.GetUserId();
            List<Order> cur = Broadcast.Get(id).Orders;
            for (int i = 0;i< cur.Count(); i++)
            {
                if(cur.ElementAt(i).UserId == UserID)
                {
                    return cur.ElementAt(i).Items;
                }
            }
            return null;
        }
        [HttpGet]
        public static void UpdateOrder(List<ItemValue> x,Guid id)
        {
            Guid UserID = UserIdentityManager.GetUserId();
            List<Order> cur = Broadcast.Get(id).Orders;
            for (int i = 0; i < cur.Count(); i++)
            {
                if (cur.ElementAt(i).UserId == UserID)
                {
                    cur.ElementAt(i).Items = x;
                }
            }
        }

        [HttpGet]
        public static void DeleteOrder(Guid id)
        {
            Guid UserID = UserIdentityManager.GetUserId();
            List<Order> cur = Broadcast.Get(id).Orders;
            for (int i = 0; i < cur.Count(); i++)
            {
                if (cur.ElementAt(i).UserId == UserID)
                {
                    cur.ElementAt(i).Items = new List<ItemValue>();
                }
            }
        }



    }
}

