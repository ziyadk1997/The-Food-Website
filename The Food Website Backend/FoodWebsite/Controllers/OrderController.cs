using FoodWebsite.DAL;
using FoodWebsite.UserIdentity;
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
            Order order = new Order
            {
                Comments = comment,
                Items = items,
                OrderID = Guid.NewGuid(),
                UserId = UserIdentityManager.GetUserId()

            };

            Order.Add(order);
        }
    }
}
