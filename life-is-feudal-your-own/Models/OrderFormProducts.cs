using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using life_is_feudal_your_own.utils;
namespace life_is_feudal_your_own.Models
{
    public class OrderFormProducts
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("OrderForm_id")]
        public long OrderForm_id { get; set; }
        [JsonProperty("ItemQuality_id")]
        public long ItemQuality_id { get; set; }
        [JsonProperty("isSelling")]
        public bool Selling { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }

        public ItemQuality GetItemQuality()
        {
            return MySqlDbApi.GetItemQualityById(ItemQuality_id);
            //return JsonConvert.DeserializeObject<ItemQuality>(LifeIsFeudalApi.callApi($"ItemQuality/{ItemQuality_id}"));
        }
        public ItemQualityType GetItemQualityType()
        {
            return MySqlDbApi.GetItemQualityTypeById(GetItemQuality().ItemQualityType_id);
            //return JsonConvert.DeserializeObject<ItemQualityType>(LifeIsFeudalApi.callApi($"ItemQualityType/{GetItemQuality().ItemQualityType_id}"));
        }

    }
}