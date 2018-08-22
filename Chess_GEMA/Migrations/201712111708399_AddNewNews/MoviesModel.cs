namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewNewsMoviesModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Length = c.String(),
                        Description = c.String(),
                        Resolution = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        SubTitel = c.String(),
                        Text = c.String(),
                        Image = c.String(),
                        Gallery = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Movies");
        }
    }
}
