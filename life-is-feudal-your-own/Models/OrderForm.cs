using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace life_is_feudal_your_own.Models
{
    public class OrderForm
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("playerName")]
        public string PlayerName { get; set; }
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; } 
        public List<OrderFormProducts> Products { get; set; }
        [JsonProperty("created_at")]
        public DateTime Created { get; set; }
        [JsonProperty("updated_at")]
        public DateTime Updated { get; set; }

        public OrderForm()
        {
            Products = new List<OrderFormProducts>();
        }
    }
}