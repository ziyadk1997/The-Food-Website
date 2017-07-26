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
        public void AddItem(String name,Guid restaurantID)
        {
            if (name != null && name != string.Empty)
            {
                Restaurant.AddItem(restaurantID, new Item { Name = name });
            }
        }

        [HttpGet]
        public List<Restaurant> GetAll()
        {
            return Restaurant.GetAll();
        }
        [HttpGet]
        public List<String> GetRestaurantItems(Guid id)
        {
            List<Item> x = Restaurant.Get(id).Items;
            if(x == null)
            {
                return new List<string>();
            }
            List<String> y = new List<String>();
            for(int i = 0; i < x.Count; i++)
            {
                y.Add(x.ElementAt(i).Name);
            }
            return y;
        }

        [HttpGet]
        public void PutPrice(Guid id,String name,double price)
        {
            Restaurant.UpdateItemPrice(id, name, price);
        }
    }
}
