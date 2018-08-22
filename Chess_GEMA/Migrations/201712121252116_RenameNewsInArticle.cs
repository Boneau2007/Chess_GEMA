namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameNewsInArticle : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.News", newName: "Articels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Articels", newName: "News");
        }
    }
}
