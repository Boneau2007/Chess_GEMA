namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booleanAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsFeeds", "Edited", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsFeeds", "Edited");
        }
    }
}
