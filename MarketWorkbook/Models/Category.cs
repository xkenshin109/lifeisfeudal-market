using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketWorkbook.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}