namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredNewsFeedModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsFeeds", "Titel", c => c.String(nullable: false));
            AlterColumn("dbo.NewsFeeds", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsFeeds", "Text", c => c.String());
            AlterColumn("dbo.NewsFeeds", "Titel", c => c.String());
        }
    }
}
