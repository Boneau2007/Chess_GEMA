namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsFeeds", "TitelImageId", "dbo.Files");
            DropIndex("dbo.NewsFeeds", new[] { "TitelImageId" });
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelImageId", newName: "TitelImage_Id");
            AlterColumn("dbo.NewsFeeds", "TitelImage_Id", c => c.Int());
            CreateIndex("dbo.NewsFeeds", "TitelImage_Id");
            AddForeignKey("dbo.NewsFeeds", "TitelImage_Id", "dbo.Files", "Id");
            DropColumn("dbo.NewsFeeds", "GalerieImagesId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsFeeds", "GalerieImagesId", c => c.Int(nullable: false));
            DropForeignKey("dbo.NewsFeeds", "TitelImage_Id", "dbo.Files");
            DropIndex("dbo.NewsFeeds", new[] { "TitelImage_Id" });
            AlterColumn("dbo.NewsFeeds", "TitelImage_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelImage_Id", newName: "TitelImageId");
            CreateIndex("dbo.NewsFeeds", "TitelImageId");
            AddForeignKey("dbo.NewsFeeds", "TitelImageId", "dbo.Files", "Id", cascadeDelete: true);
        }
    }
}
