using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class RestaurantDataModel
    {
        [Key]
        public Guid RestaurantID { set; get; }

        public String Name { set; get; }

        public string SerializedItems { get; set; }

        public static RestaurantDataModel Create (Restaurant restaurant)
        {
            return new RestaurantDataModel
            {
                Name = restaurant.Name,
                RestaurantID = restaurant.RestaurantID,
                SerializedItems = JsonConvert.SerializeObject(restaurant.Items)
            };
        }
    }
}
