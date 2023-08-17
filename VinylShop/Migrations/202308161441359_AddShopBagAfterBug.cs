namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopBagAfterBug : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopBags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUser_Id = c.Int(),
                        music_Records_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser_Id)
                .ForeignKey("dbo.Music_Records", t => t.music_Records_Id)
                .Index(t => t.IdUser_Id)
                .Index(t => t.music_Records_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopBags", "music_Records_Id", "dbo.Music_Records");
            DropForeignKey("dbo.ShopBags", "IdUser_Id", "dbo.Users");
            DropIndex("dbo.ShopBags", new[] { "music_Records_Id" });
            DropIndex("dbo.ShopBags", new[] { "IdUser_Id" });
            DropTable("dbo.ShopBags");
        }
    }
}
