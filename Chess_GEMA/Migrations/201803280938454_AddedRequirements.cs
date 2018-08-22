namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequirements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsFeeds", "GalerieImages_Id", c => c.Int());
            CreateIndex("dbo.NewsFeeds", "GalerieImages_Id");
            AddForeignKey("dbo.NewsFeeds", "GalerieImages_Id", "dbo.Galleries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeeds", "GalerieImages_Id", "dbo.Galleries");
            DropIndex("dbo.NewsFeeds", new[] { "GalerieImages_Id" });
            DropColumn("dbo.NewsFeeds", "GalerieImages_Id");
        }
    }
}
