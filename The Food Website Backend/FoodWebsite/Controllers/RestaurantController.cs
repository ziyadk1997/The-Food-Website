using FoodWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class RestaurantController : ApiController
    {
        [HttpGet]
        public void Add(String restaurantName)
        {
            Restaurant restaurant = new Restaurant
            {

                RestaurantID = Guid.NewGuid(),
                Name = restaurantName
            };
            Restaurant.Add(restaurant.RestaurantID,restaurant);
        }
        [HttpGet]
        public void AddItem(Guid itemID,Guid restaurantID)
        {
            Item item = Item.Get(itemID);
            Restaurant.AddItem(restaurantID, item);
        }
    }
}
