using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace CoreManagement.Models
{
    public class OrderFormProduct
    {
        public long Id { get; set; }
        [ForeignKey("OrderForm")]
        public long OrderForm_Id { get; set; }
        public virtual OrderForm OrderForm { get; set; }
        [ForeignKey("ItemQuality")]
        public long ItemQuality_Id { get; set; }
        public virtual ItemQuality ItemQuality { get; set; }
        public bool Selling { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }
    }
}