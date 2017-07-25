using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class ItemValue
    {
        public Item Item { set; get; }
        
        public int Quantity { set; get; }

        public String Comments { set; get; }
    }
}
