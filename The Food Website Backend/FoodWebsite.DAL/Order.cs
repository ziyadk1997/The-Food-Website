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
        public List<ItemValue> Items { set; get; }
    }
}
