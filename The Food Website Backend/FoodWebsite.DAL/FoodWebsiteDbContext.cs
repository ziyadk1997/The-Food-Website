using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class FoodWebsiteDbContext : DbContext
    {

        public FoodWebsiteDbContext() : base("name=ConnectionString")
        {

        }

        public DbSet<RestaurantDataModel> RestaurantDataModels { get; set; }
    }
}
