using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreManagement.Models
{
    public class Item
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }

        [ForeignKey("Category")]
        public long Category_Id { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public long SubCategory_Id { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<ItemQuality> Qualities { get; set; }
        public DateTime Created { get; set; }
        public Item()
        {
            Qualities = new List<ItemQuality>();
        }
    }

}