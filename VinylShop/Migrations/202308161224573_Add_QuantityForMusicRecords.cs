namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_QuantityForMusicRecords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Music_Records", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Music_Records", "Quantity");
        }
    }
}
