using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public  class Item
    {
        private static Dictionary<Guid,Item> Items { set; get; } = new Dictionary<Guid, Item>();
        public Guid ItemID { set; get; }
        public String Name { set; get; }
        public double Price { set; get; }

        public static void Add(Guid id,Item item)
        {
            Items.Add(id, item);
        }
        public static Item Get(Guid id)
        {
            return Items[id];
        }
    }
}
