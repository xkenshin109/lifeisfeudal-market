namespace CoreManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class override_feature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemQualities", "OverridePrice", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemQualities", "OverridePrice");
        }
    }
}
