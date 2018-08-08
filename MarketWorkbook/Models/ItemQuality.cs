using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketWorkbook.Models
{
    public class ItemQuality
    {
        public long Id { get; set; }
        public long Item_Id { get; set; }
        public virtual Item Item { get; set; }
        public long ItemQualityType_id { get; set; }
        public virtual ItemQualityType ItemQualityType { get; set; }
        public bool BuyActive { get; set; }
        public bool SellActive { get; set; }
        public DateTime Created { get; set; }

    }
}