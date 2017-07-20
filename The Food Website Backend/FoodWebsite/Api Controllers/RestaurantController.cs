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
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");

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
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            Restaurant.AddItem(restaurantID, new Item { Name = name, ItemID = Guid.NewGuid() });
        }
        public List<Restaurant> GetAll()
        {
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Headers", "access-control-allow-origin,content-type");
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Methods", "GET");
            return Restaurant.GetAll();
        }
    }
}
