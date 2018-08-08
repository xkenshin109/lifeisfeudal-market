using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MarketWorkbook.Models
{
    public class OrderFormProduct
    {
        public long Id { get; set; }
        public long OrderForm_id { get; set; }
        public virtual OrderForm OrderForm { get; set; }
        public long ItemQuality_id { get; set; }
        public virtual ItemQuality ItemQuality { get; set; }
        public bool Selling { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
    }
}