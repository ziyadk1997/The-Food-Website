﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Restaurant
    {
        private static Dictionary<Guid, Restaurant> Restaurants { set; get; } = new Dictionary<Guid, Restaurant>();
        public Guid RestaurantID { set; get; }
        public String Name { set; get; }
        public List<Item> Items { set; get; } = new List<Item>();

        public static void Add(Guid id, Restaurant res)
        {
            Restaurants.Add(id, res);
        }
        public static void AddItem(Guid id, Item item)
        {
            Restaurants[id].Items.Add(item);
        }

        public static Restaurant Get (Guid id)
        {
            return Restaurants[id];
        }

        public static List<Restaurant> GetAll()
        {
            return Restaurants.ToList().Select(Restaurants => Restaurants.Value).ToList();
        }
    }
}
