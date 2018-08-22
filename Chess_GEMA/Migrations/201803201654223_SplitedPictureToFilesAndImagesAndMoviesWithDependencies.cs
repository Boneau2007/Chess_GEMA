namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SplitedPictureToFilesAndImagesAndMoviesWithDependencies : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pictures", newName: "Images");
            RenameColumn(table: "dbo.Galleries", name: "TitelPicture_Id", newName: "TitelImage_Id");
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelPicture_Id", newName: "TitelImage_Id");
            RenameIndex(table: "dbo.Galleries", name: "IX_TitelPicture_Id", newName: "IX_TitelImage_Id");
            RenameIndex(table: "dbo.NewsFeeds", name: "IX_TitelPicture_Id", newName: "IX_TitelImage_Id");
            AddColumn("dbo.Images", "Description", c => c.String());
            AddColumn("dbo.Images", "Length", c => c.String());
            AddColumn("dbo.Images", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Movies");
        }
        
        public override void Down()
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
            
            DropColumn("dbo.Images", "Discriminator");
            DropColumn("dbo.Images", "Length");
            DropColumn("dbo.Images", "Description");
            RenameIndex(table: "dbo.NewsFeeds", name: "IX_TitelImage_Id", newName: "IX_TitelPicture_Id");
            RenameIndex(table: "dbo.Galleries", name: "IX_TitelImage_Id", newName: "IX_TitelPicture_Id");
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelImage_Id", newName: "TitelPicture_Id");
            RenameColumn(table: "dbo.Galleries", name: "TitelImage_Id", newName: "TitelPicture_Id");
            RenameTable(name: "dbo.Images", newName: "Pictures");
        }
    }
}
