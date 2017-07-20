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
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");

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
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            Broadcast.Close(id);
        }
        [HttpGet]
        public List<Broadcast> History()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");

            return Broadcast.GetAll().Where(e => e.Active == false).ToList();
        }
        [HttpGet]
        public void AddOrder(Order order,Guid id)
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            Broadcast.Get(id).Orders.Add(order);
        }
        [HttpGet]
        public List<Order> Reciept(Guid id)
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            return Broadcast.Get(id).Orders;
        }

        [HttpGet]
        public List<Broadcast> Active()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");

            return Broadcast.GetAll().Where(e => e.Active == true).ToList();
        }
        
    }
}

