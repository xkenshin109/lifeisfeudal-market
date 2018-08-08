namespace CoreManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LifeIsFeudalDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LifeIsFeudalDb context)
        {
            context.Configurations.AddOrUpdate(new Models.Configuration
            {
                Id = 1,
                Key ="Email",
                Value = "jeremyfoster07@yahoo.com"
            });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
