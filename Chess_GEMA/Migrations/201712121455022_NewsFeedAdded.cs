namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsFeedAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsFeeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Date = c.String(),
                        Moderator = c.String(),
                        Text = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsFeeds");
        }
    }
}
