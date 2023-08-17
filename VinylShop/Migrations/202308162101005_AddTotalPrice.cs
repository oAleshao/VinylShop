namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalPrice");
        }
    }
}
