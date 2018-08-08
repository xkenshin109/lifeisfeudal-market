using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketWorkbook.Models
{
    public class OrderForm
    {
        public long Id { get; set; }
        public string PlayerName { get; set; }
        public string OrderNumber { get; set; } 
        public virtual ICollection<OrderFormProduct> Products { get; set; }
        public DateTime Created { get; set; }

        public OrderForm()
        {
            Products = new List<OrderFormProduct>();
        }
    }
}