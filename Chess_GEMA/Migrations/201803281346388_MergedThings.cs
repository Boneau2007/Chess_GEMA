namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergedThings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Text", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Questions", "Topic", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Questions", "Text", c => c.String(maxLength: 513));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeeds", "GalerieImages_Id", "dbo.Galleries");
            DropForeignKey("dbo.Files", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.NewsFeeds", new[] { "GalerieImages_Id" });
            DropIndex("dbo.Files", new[] { "Gallery_Id" });
            AlterColumn("dbo.Questions", "Text", c => c.String());
            AlterColumn("dbo.Questions", "Topic", c => c.String());
            AlterColumn("dbo.Answers", "Text", c => c.String());
            DropColumn("dbo.NewsFeeds", "GalerieImages_Id");
            DropColumn("dbo.Files", "Gallery_Id");
        }
    }
}
