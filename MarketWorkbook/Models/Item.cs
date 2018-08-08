using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketWorkbook.Models
{
    public class Item
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public long Category_id { get; set; }
        public virtual Category Category { get; set; }
        public long SubCategory_id { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<ItemQuality> Qualities { get; set; }
        public DateTime Created { get; set; }
        public Item()
        {
            Qualities = new List<ItemQuality>();
        }
    }

}