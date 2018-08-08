namespace CoreManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class item_free : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemQualities", "Free", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemQualities", "Free");
        }
    }
}
