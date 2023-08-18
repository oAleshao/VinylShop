namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SellDaysTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SellsDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        startDayForSell = c.DateTime(nullable: false),
                        endDayForSell = c.DateTime(nullable: false),
                        Sell = c.Double(nullable: false),
                        ganre_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GanreMusics", t => t.ganre_Id)
                .Index(t => t.ganre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SellsDays", "ganre_Id", "dbo.GanreMusics");
            DropIndex("dbo.SellsDays", new[] { "ganre_Id" });
            DropTable("dbo.SellsDays");
        }
    }
}
