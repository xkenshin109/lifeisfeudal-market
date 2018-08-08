using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreManagement.Models;
namespace CoreManagement
{
    using System.Data.Entity;
    public class LifeIsFeudalDb: DbContext
    {
        public LifeIsFeudalDb() : base("name=LifeIsFeudalDb")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; } 
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemQuality> ItemQualities { get; set; }
        public virtual DbSet<ItemQualityType> ItemQualityTypes { get; set; }
        public virtual DbSet<OrderForm> OrderForms { get; set; }
        public virtual DbSet<OrderFormProduct> OrderFormProducts { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
