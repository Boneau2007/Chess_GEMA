namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedThumbmailBytesAndSizeBytsInPictures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "SizeBytes", c => c.Binary());
            AddColumn("dbo.Pictures", "ThumbnailBytes", c => c.Binary());
            AlterColumn("dbo.Pictures", "Format", c => c.Int(nullable: false));
            DropColumn("dbo.Pictures", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Size", c => c.String());
            AlterColumn("dbo.Pictures", "Format", c => c.String());
            DropColumn("dbo.Pictures", "ThumbnailBytes");
            DropColumn("dbo.Pictures", "SizeBytes");
        }
    }
}
