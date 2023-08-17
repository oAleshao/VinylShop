namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrdersAndShopBags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "music_Records_Id", "dbo.Music_Records");
            DropForeignKey("dbo.ShopBagProducts", "ShopBag_Id", "dbo.ShopBags");
            DropForeignKey("dbo.ShopBagProducts", "Product_Id", "dbo.Products");
            DropIndex("dbo.Products", new[] { "music_Records_Id" });
            DropIndex("dbo.ShopBagProducts", new[] { "ShopBag_Id" });
            DropIndex("dbo.ShopBagProducts", new[] { "Product_Id" });
            CreateTable(
                "dbo.HelpOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        music_Records_Id = c.Int(),
                        orders_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music_Records", t => t.music_Records_Id)
                .ForeignKey("dbo.Orders", t => t.orders_Id)
                .Index(t => t.music_Records_Id)
                .Index(t => t.orders_Id);
            
            AddColumn("dbo.Music_Records", "ShopBag_Id", c => c.Int());
            AddColumn("dbo.Orders", "dateOrder", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Music_Records", "ShopBag_Id");
            AddForeignKey("dbo.Music_Records", "ShopBag_Id", "dbo.ShopBags", "Id");
            DropTable("dbo.Products");
            DropTable("dbo.ShopBagProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShopBagProducts",
                c => new
                    {
                        ShopBag_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopBag_Id, t.Product_Id });
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        music_Records_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Music_Records", "ShopBag_Id", "dbo.ShopBags");
            DropForeignKey("dbo.HelpOrders", "orders_Id", "dbo.Orders");
            DropForeignKey("dbo.HelpOrders", "music_Records_Id", "dbo.Music_Records");
            DropIndex("dbo.HelpOrders", new[] { "orders_Id" });
            DropIndex("dbo.HelpOrders", new[] { "music_Records_Id" });
            DropIndex("dbo.Music_Records", new[] { "ShopBag_Id" });
            DropColumn("dbo.Orders", "dateOrder");
            DropColumn("dbo.Music_Records", "ShopBag_Id");
            DropTable("dbo.HelpOrders");
            CreateIndex("dbo.ShopBagProducts", "Product_Id");
            CreateIndex("dbo.ShopBagProducts", "ShopBag_Id");
            CreateIndex("dbo.Products", "music_Records_Id");
            AddForeignKey("dbo.ShopBagProducts", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ShopBagProducts", "ShopBag_Id", "dbo.ShopBags", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "music_Records_Id", "dbo.Music_Records", "Id");
        }
    }
}
