using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketWorkbook.Models
{
    public class ItemQualityType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? BuyMultiplier { get; set; }
        public decimal? SellMultiplier { get; set; }
        public DateTime Updated { get; set; }
    }
}