namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsFeeds", "TitelImage_Id", "dbo.Files");
            DropIndex("dbo.NewsFeeds", new[] { "TitelImage_Id" });
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelImage_Id", newName: "TitelImageId");
            AddColumn("dbo.NewsFeeds", "GalerieImagesId", c => c.Int(nullable: false));
            AlterColumn("dbo.NewsFeeds", "TitelImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.NewsFeeds", "TitelImageId");
            AddForeignKey("dbo.NewsFeeds", "TitelImageId", "dbo.Files", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeeds", "TitelImageId", "dbo.Files");
            DropIndex("dbo.NewsFeeds", new[] { "TitelImageId" });
            AlterColumn("dbo.NewsFeeds", "TitelImageId", c => c.Int());
            DropColumn("dbo.NewsFeeds", "GalerieImagesId");
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelImageId", newName: "TitelImage_Id");
            CreateIndex("dbo.NewsFeeds", "TitelImage_Id");
            AddForeignKey("dbo.NewsFeeds", "TitelImage_Id", "dbo.Files", "Id");
        }
    }
}
