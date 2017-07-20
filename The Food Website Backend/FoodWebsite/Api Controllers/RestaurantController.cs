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
        public void AddItem(string name,Guid restaurantID)
        {
            Restaurant.AddItem(restaurantID, new Item { Name = name});
        }
        public List<Restaurant> GetAll()
        {
            return Restaurant.GetAll();
        }
    }
}
