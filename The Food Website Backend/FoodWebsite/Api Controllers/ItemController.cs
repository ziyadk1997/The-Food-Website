using FoodWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class ItemController : ApiController
    {
        [HttpGet]
        public void Add(String name,double price)
        {
            Item item = new Item
            {
                Name = name,
                Price = price,
                ItemID = Guid.NewGuid(),
            };
            Item.Add(item.ItemID,item);
        }
    }
}
