namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeShopBag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShopBags", "IdUser_Id", "dbo.Users");
            DropForeignKey("dbo.Music_Records", "ShopBag_Id", "dbo.ShopBags");
            DropIndex("dbo.Music_Records", new[] { "ShopBag_Id" });
            DropIndex("dbo.ShopBags", new[] { "IdUser_Id" });
            DropColumn("dbo.Music_Records", "ShopBag_Id");
            DropTable("dbo.ShopBags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShopBags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Music_Records", "ShopBag_Id", c => c.Int());
            CreateIndex("dbo.ShopBags", "IdUser_Id");
            CreateIndex("dbo.Music_Records", "ShopBag_Id");
            AddForeignKey("dbo.Music_Records", "ShopBag_Id", "dbo.ShopBags", "Id");
            AddForeignKey("dbo.ShopBags", "IdUser_Id", "dbo.Users", "Id");
        }
    }
}
