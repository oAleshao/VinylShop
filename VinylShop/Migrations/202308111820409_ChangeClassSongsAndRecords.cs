namespace VinylShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClassSongsAndRecords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "IdMusic_Records_Id", "dbo.Music_Records");
            DropIndex("dbo.Songs", new[] { "IdMusic_Records_Id" });
            CreateTable(
                "dbo.Music_RecordsSongs",
                c => new
                    {
                        Music_Records_Id = c.Int(nullable: false),
                        Songs_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Music_Records_Id, t.Songs_Id })
                .ForeignKey("dbo.Music_Records", t => t.Music_Records_Id, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Songs_Id, cascadeDelete: true)
                .Index(t => t.Music_Records_Id)
                .Index(t => t.Songs_Id);
            
            DropColumn("dbo.Songs", "IdMusic_Records_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "IdMusic_Records_Id", c => c.Int());
            DropForeignKey("dbo.Music_RecordsSongs", "Songs_Id", "dbo.Songs");
            DropForeignKey("dbo.Music_RecordsSongs", "Music_Records_Id", "dbo.Music_Records");
            DropIndex("dbo.Music_RecordsSongs", new[] { "Songs_Id" });
            DropIndex("dbo.Music_RecordsSongs", new[] { "Music_Records_Id" });
            DropTable("dbo.Music_RecordsSongs");
            CreateIndex("dbo.Songs", "IdMusic_Records_Id");
            AddForeignKey("dbo.Songs", "IdMusic_Records_Id", "dbo.Music_Records", "Id");
        }
    }
}
