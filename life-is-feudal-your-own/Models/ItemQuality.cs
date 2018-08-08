using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.Models
{
    public class ItemQuality
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("Item_id")]
        public long Item_Id { get; set; }
        [JsonProperty("ItemQualityType_id")]
        public long ItemQualityType_id { get; set; }
        [JsonProperty("buy_active")]
        public bool BuyActive { get; set; }
        [JsonProperty("sell_active")]
        public bool SellActive { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public Item GetItem()
        {
            return JsonConvert.DeserializeObject<Item>(life_is_feudal_your_own.utils.LifeIsFeudalApi.callApi($"Item/{Item_Id}"));
        }
    }
}