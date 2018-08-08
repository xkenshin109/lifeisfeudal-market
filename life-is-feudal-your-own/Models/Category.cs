using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public long id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }
    }
}