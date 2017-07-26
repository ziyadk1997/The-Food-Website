using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Order
    {
        public Guid UserId { set; get; }

        private List<ItemValue> items { set; get; }

        public void SetItems (List<ItemValue> items)
        {
            this.items = items;
        }
        
        public List<ItemValue> GetItems(Guid restaurantId)
        {
            List<Item> restItems = Restaurant.Get(restaurantId).Items;
            for (int i = 0; i<this.items.Count; i++)
            {
                var item = restItems.Where(localItem => localItem.Name == this.items[i].Item.Name).FirstOrDefault();
                this.items[i].Item.Price = item.Price;
            }
            return this.items;
        }
    }
}
