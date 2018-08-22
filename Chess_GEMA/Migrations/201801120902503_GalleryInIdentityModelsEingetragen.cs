namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryInIdentityModelsEingetragen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Owner = c.String(),
                        Date = c.DateTime(nullable: false),
                        TitelPicture_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.TitelPicture_Id)
                .Index(t => t.TitelPicture_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Galleries", "TitelPicture_Id", "dbo.Pictures");
            DropIndex("dbo.Galleries", new[] { "TitelPicture_Id" });
            DropTable("dbo.Galleries");
        }
    }
}
