namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExecutorMusics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IdMusic_Records_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music_Records", t => t.IdMusic_Records_Id)
                .Index(t => t.IdMusic_Records_Id);
            
            CreateTable(
                "dbo.GanreMusics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Music_Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountSongs = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        SellingPrice = c.Double(nullable: false),
                        Description = c.String(),
                        pubishHouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PubishHouses", t => t.pubishHouse_Id)
                .Index(t => t.pubishHouse_Id);
            
            CreateTable(
                "dbo.PubishHouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Iduser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Iduser_Id)
                .Index(t => t.Iduser_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Email = c.String(),
                        Login = c.String(),
                        Password = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Adres = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        music_Records_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Music_Records", t => t.music_Records_Id)
                .Index(t => t.music_Records_Id);
            
            CreateTable(
                "dbo.ShopBags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser_Id)
                .Index(t => t.IdUser_Id);
            
            CreateTable(
                "dbo.SongsExecutorMusics",
                c => new
                    {
                        Songs_Id = c.Int(nullable: false),
                        ExecutorMusic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Songs_Id, t.ExecutorMusic_Id })
                .ForeignKey("dbo.Songs", t => t.Songs_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExecutorMusics", t => t.ExecutorMusic_Id, cascadeDelete: true)
                .Index(t => t.Songs_Id)
                .Index(t => t.ExecutorMusic_Id);
            
            CreateTable(
                "dbo.GanreMusicSongs",
                c => new
                    {
                        GanreMusic_Id = c.Int(nullable: false),
                        Songs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GanreMusic_Id, t.Songs_Id })
                .ForeignKey("dbo.GanreMusics", t => t.GanreMusic_Id, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Songs_Id, cascadeDelete: true)
                .Index(t => t.GanreMusic_Id)
                .Index(t => t.Songs_Id);
            
            CreateTable(
                "dbo.ShopBagProducts",
                c => new
                    {
                        ShopBag_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShopBag_Id, t.Product_Id })
                .ForeignKey("dbo.ShopBags", t => t.ShopBag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ShopBag_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopBagProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ShopBagProducts", "ShopBag_Id", "dbo.ShopBags");
            DropForeignKey("dbo.ShopBags", "IdUser_Id", "dbo.Users");
            DropForeignKey("dbo.Products", "music_Records_Id", "dbo.Music_Records");
            DropForeignKey("dbo.Orders", "Iduser_Id", "dbo.Users");
            DropForeignKey("dbo.Songs", "IdMusic_Records_Id", "dbo.Music_Records");
            DropForeignKey("dbo.Music_Records", "pubishHouse_Id", "dbo.PubishHouses");
            DropForeignKey("dbo.GanreMusicSongs", "Songs_Id", "dbo.Songs");
            DropForeignKey("dbo.GanreMusicSongs", "GanreMusic_Id", "dbo.GanreMusics");
            DropForeignKey("dbo.SongsExecutorMusics", "ExecutorMusic_Id", "dbo.ExecutorMusics");
            DropForeignKey("dbo.SongsExecutorMusics", "Songs_Id", "dbo.Songs");
            DropIndex("dbo.ShopBagProducts", new[] { "Product_Id" });
            DropIndex("dbo.ShopBagProducts", new[] { "ShopBag_Id" });
            DropIndex("dbo.GanreMusicSongs", new[] { "Songs_Id" });
            DropIndex("dbo.GanreMusicSongs", new[] { "GanreMusic_Id" });
            DropIndex("dbo.SongsExecutorMusics", new[] { "ExecutorMusic_Id" });
            DropIndex("dbo.SongsExecutorMusics", new[] { "Songs_Id" });
            DropIndex("dbo.ShopBags", new[] { "IdUser_Id" });
            DropIndex("dbo.Products", new[] { "music_Records_Id" });
            DropIndex("dbo.Orders", new[] { "Iduser_Id" });
            DropIndex("dbo.Music_Records", new[] { "pubishHouse_Id" });
            DropIndex("dbo.Songs", new[] { "IdMusic_Records_Id" });
            DropTable("dbo.ShopBagProducts");
            DropTable("dbo.GanreMusicSongs");
            DropTable("dbo.SongsExecutorMusics");
            DropTable("dbo.ShopBags");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.PubishHouses");
            DropTable("dbo.Music_Records");
            DropTable("dbo.GanreMusics");
            DropTable("dbo.Songs");
            DropTable("dbo.ExecutorMusics");
            DropTable("dbo.Admins");
        }
    }
}
