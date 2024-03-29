﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreManagement.Models
{
    public class ItemQualityType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal? BuyMultiplier { get; set; }
        public decimal? SellMultiplier { get; set; }
        public DateTime Updated { get; set; }
    }
}