using life_is_feudal_your_own.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public int? Price { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }
        [JsonProperty("category_id")]
        public long Category_id { get; set; }
        [JsonProperty("subcategory_id")]
        public long SubCategory_id { get; set; }
        public List<ItemQuality> Qualities { get; set; }
        public Item()
        {
            Qualities = new List<ItemQuality>();
        }
        public Item Save()
        {
            //var a = "";
            //if(Id != 0)
            //{
            //    var data = new {
            //        id = Id,
            //        price = Price,
            //        name = Name
            //    };
            //    a = life_is_feudal_your_own.utils.LifeIsFeudalApi.callApiPut($"Item/{Id}", JsonConvert.SerializeObject(data));
            //}
            //else
            //{
            //    var data = new
            //    {
            //        price = Price,
            //        name = Name
            //    };
            //    a = life_is_feudal_your_own.utils.LifeIsFeudalApi.callApiPost($"Item/", JsonConvert.SerializeObject(data));
            //}

            //return a;
            var self = this;
            var a = MySqlDbApi.SaveItem(ref self);
            return self;
        }

    }

}