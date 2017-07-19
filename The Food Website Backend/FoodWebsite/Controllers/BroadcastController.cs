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
        public void AddOrder(Order order,Guid id)
        {
            Broadcast.Get(id).Orders.Add(order);
        }
        [HttpGet]
        public List<Order> GetOrder(Guid id)
        {
            return Broadcast.Get(id).Orders;
        }
        [HttpGet]
        public List<Order> Reciept(Guid id)
        {
            return Broadcast.Get(id).Orders;
        }

        [HttpGet]
        public List<Broadcast> Active()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            var dummyBroadcast1 = new Broadcast
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
            var dummyBroadcast2 = new Broadcast
            {
                User = new DAL.User
                {
                    Name = "ziyad"
                },
                Restaurant = new Restaurant
                {
                    Name = "PapaJohns"
                },
                Deadline = DateTime.Now
            };
            var dummyBroadcast4 = new Broadcast
            {
                User = new DAL.User
                {
                    Name = "Zare3"
                },
                Restaurant = new Restaurant
                {
                    Name = "KFC"
                },
                Deadline = DateTime.Now,
                Active = false
            };
            var dummyBroadcast3 = new Broadcast
            {
                User = new DAL.User
                {
                    Name = "Lotfy"
                },
                Restaurant = new Restaurant
                {
                    Name = "PizzaHut"
                },
                Deadline = DateTime.Now
            };

            return new List<Broadcast> { dummyBroadcast1, dummyBroadcast2, dummyBroadcast3 }.Where(e => e.Active == true).ToList();
            //return Broadcast.GetAll().Where(e => e.Active == true).ToList();
        }
        
    }
}

