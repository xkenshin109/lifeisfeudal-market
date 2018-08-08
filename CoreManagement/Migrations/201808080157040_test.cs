namespace CoreManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemQualities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Item_Id = c.Long(nullable: false),
                        ItemQualityType_Id = c.Long(nullable: false),
                        BuyActive = c.Boolean(nullable: false),
                        SellActive = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.ItemQualityTypes", t => t.ItemQualityType_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.ItemQualityType_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(),
                        Category_Id = c.Long(nullable: false),
                        SubCategory_Id = c.Long(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.SubCategory_Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemQualityTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        BuyMultiplier = c.Decimal(precision: 18, scale: 2),
                        SellMultiplier = c.Decimal(precision: 18, scale: 2),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderFormProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderForm_Id = c.Long(nullable: false),
                        ItemQuality_Id = c.Long(nullable: false),
                        Selling = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemQualities", t => t.ItemQuality_Id, cascadeDelete: true)
                .ForeignKey("dbo.OrderForms", t => t.OrderForm_Id, cascadeDelete: true)
                .Index(t => t.OrderForm_Id)
                .Index(t => t.ItemQuality_Id);
            
            CreateTable(
                "dbo.OrderForms",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PlayerName = c.String(),
                        OrderNumber = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderFormProducts", "OrderForm_Id", "dbo.OrderForms");
            DropForeignKey("dbo.OrderFormProducts", "ItemQuality_Id", "dbo.ItemQualities");
            DropForeignKey("dbo.ItemQualities", "ItemQualityType_Id", "dbo.ItemQualityTypes");
            DropForeignKey("dbo.ItemQualities", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Items", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.Items", "Category_Id", "dbo.Categories");
            DropIndex("dbo.OrderFormProducts", new[] { "ItemQuality_Id" });
            DropIndex("dbo.OrderFormProducts", new[] { "OrderForm_Id" });
            DropIndex("dbo.Items", new[] { "SubCategory_Id" });
            DropIndex("dbo.Items", new[] { "Category_Id" });
            DropIndex("dbo.ItemQualities", new[] { "ItemQualityType_Id" });
            DropIndex("dbo.ItemQualities", new[] { "Item_Id" });
            DropTable("dbo.OrderForms");
            DropTable("dbo.OrderFormProducts");
            DropTable("dbo.ItemQualityTypes");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Items");
            DropTable("dbo.ItemQualities");
            DropTable("dbo.Configurations");
            DropTable("dbo.Categories");
        }
    }
}
