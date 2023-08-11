namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelAdmin_Requaried : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "login", c => c.String());
            AlterColumn("dbo.Admins", "password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "login", c => c.String(nullable: false));
        }
    }
}
