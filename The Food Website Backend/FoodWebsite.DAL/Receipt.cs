using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class Receipt
    {
        public List<ReceiptItem> ReceiptItems { get; set; }

        public double Total { get; set; }
    }
}
