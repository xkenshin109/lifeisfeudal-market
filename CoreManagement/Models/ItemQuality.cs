using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreManagement.Models
{
    public class ItemQuality
    {
        public long Id { get; set; }
        [ForeignKey("Item")]
        public long Item_Id { get; set; }
        public virtual Item Item { get; set; }
        [ForeignKey("ItemQualityType")]
        public long ItemQualityType_Id { get; set; }
        public virtual ItemQualityType ItemQualityType { get; set; }
        public bool BuyActive { get; set; }
        public bool SellActive { get; set; }
        public bool Free { get;set; }
        public DateTime Created { get; set; }

    }
}