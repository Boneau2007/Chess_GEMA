namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTheNewsFeedModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsFeeds", "TitalPicture_Id", c => c.Int());
            AlterColumn("dbo.NewsFeeds", "Date", c => c.DateTime(nullable: false));
            CreateIndex("dbo.NewsFeeds", "TitalPicture_Id");
            AddForeignKey("dbo.NewsFeeds", "TitalPicture_Id", "dbo.Pictures", "Id");
            DropColumn("dbo.NewsFeeds", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsFeeds", "Image", c => c.String());
            DropForeignKey("dbo.NewsFeeds", "TitalPicture_Id", "dbo.Pictures");
            DropIndex("dbo.NewsFeeds", new[] { "TitalPicture_Id" });
            AlterColumn("dbo.NewsFeeds", "Date", c => c.String());
            DropColumn("dbo.NewsFeeds", "TitalPicture_Id");
        }
    }
}
