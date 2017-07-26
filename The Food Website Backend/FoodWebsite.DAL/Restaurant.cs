using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Restaurant
    {
        public Guid RestaurantID { set; get; }
        public String Name { set; get; }
        public List<Item> Items { get; set; }

        public static void Add(Guid id, Restaurant res)
        {
            using (var context = new FoodWebsiteDbContext())
            {
                RestaurantDataModel restaurantDataModel = RestaurantDataModel.Create(res);
                context.RestaurantDataModels.Add(restaurantDataModel);
                context.SaveChanges();
            }
        }

        public static Restaurant Create(RestaurantDataModel dataModel)
        {
            Restaurant restaurant = new Restaurant
            {
                Name = dataModel.Name,
                RestaurantID = dataModel.RestaurantID,
                Items = JsonConvert.DeserializeObject<List<Item>>(dataModel.SerializedItems)
            };
            return restaurant;
        }

        public static List<Item> GetRestaurantItems(Guid restId)
        {
            var restaurant = Restaurant.Get(restId);
            return restaurant.Items;
        }

        public static void AddItem(Guid id, Item item)
        {
            using (var context = new FoodWebsiteDbContext())
            {
                RestaurantDataModel dataModel = context.RestaurantDataModels.Find(id);
                Restaurant businesessModel = Restaurant.Create(dataModel);
                if(businesessModel.Items == null)
                {
                    businesessModel.Items = new List<Item>();
                }
                businesessModel.Items.Add(item);
                RestaurantDataModel updatedDataModel = RestaurantDataModel.Create(businesessModel);
                context.RestaurantDataModels.AddOrUpdate(updatedDataModel);
                context.SaveChanges();
            }
        }

        public static void UpdateItemPrice(Guid restaurantId, string itemName, double price)
        {
            using (var context = new FoodWebsiteDbContext())
            {
                RestaurantDataModel dataModel = context.RestaurantDataModels.Find(restaurantId);
                Restaurant businesessModel = Restaurant.Create(dataModel);
                for (int i = 0; i<businesessModel.Items.Count; i++)
                {
                    var item = businesessModel.Items[i];
                    if(item.Name == itemName)
                    {
                        item.Price = price;
                    }
                }
                RestaurantDataModel updatedDataModel = RestaurantDataModel.Create(businesessModel);
                context.RestaurantDataModels.AddOrUpdate(updatedDataModel);
                context.SaveChanges();
            }
        }

        public static Restaurant Get (Guid id)
        {
            using (var context = new FoodWebsiteDbContext())
            {
                RestaurantDataModel dataModel = context.RestaurantDataModels.Find(id);
                return Restaurant.Create(dataModel);
            }
        }

        public static List<Restaurant> GetAll()
        {
            using (var context = new FoodWebsiteDbContext())
            {
                List<RestaurantDataModel> restaurantDataModels = context.RestaurantDataModels.ToList();
                return restaurantDataModels.Select(rest => Restaurant.Create(rest)).ToList();
            }
        }
    }
}
