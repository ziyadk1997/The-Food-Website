using FoodWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public void Add(String comment,Dictionary<Guid,int> items,Guid userID)
        {
            DAL.User user = DAL.User.Get(userID);
            Order order = new Order
            {
                Comments = comment,
                Items = items,
                OrderID = Guid.NewGuid(),
                User = user
            };

            Order.Add(order);
        }
    }
}
