using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.Models
{
    public class ItemQualityType
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("buy_multiplier")]
        public decimal? BuyMultiplier { get; set; }
        [JsonProperty("sell_multiplier")]
        public decimal? SellMultiplier { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }
    }
}