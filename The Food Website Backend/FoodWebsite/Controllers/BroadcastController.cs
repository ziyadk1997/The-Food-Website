using FoodWebsite.DAL;
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
        public void Add(Guid userID, Guid restaurantID, DateTime deadline)
        {
            DAL.User user = DAL.User.Get(userID);
            Restaurant restaurant = DAL.Restaurant.Get(restaurantID);

            Broadcast broadcast = new Broadcast
            {
                Restaurant = restaurant,
                Active = true,
                User = user,
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
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");

            var dummyBroadcast = new Broadcast
            {
                User = new DAL.User
                {
                    Name = "Zare3"
                },
                Restaurant = new Restaurant
                {
                    Name = "KFC"
                },
                Deadline = DateTime.Now
            };

            return new List<Broadcast> { dummyBroadcast };

            // return Broadcast.GetAll().Where(e => e.Active == false).ToList();
        }

        [HttpGet]
        public List<Order> Reciept(Guid id)
        {
            return Broadcast.Get(id).Orders;
        }
    }
}

