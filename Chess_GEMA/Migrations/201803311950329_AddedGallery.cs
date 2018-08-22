namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGallery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ReferenzGallery_Id", c => c.Int());
            CreateIndex("dbo.Files", "ReferenzGallery_Id");
            AddForeignKey("dbo.Files", "ReferenzGallery_Id", "dbo.Galleries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "ReferenzGallery_Id", "dbo.Galleries");
            DropIndex("dbo.Files", new[] { "ReferenzGallery_Id" });
            DropColumn("dbo.Files", "ReferenzGallery_Id");
        }
    }
}
