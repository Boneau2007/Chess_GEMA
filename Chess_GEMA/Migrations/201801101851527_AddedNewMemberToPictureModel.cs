namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewMemberToPictureModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Pictures", "Bytes", c => c.Binary());
            AddColumn("dbo.Pictures", "resolution", c => c.String());
            AlterColumn("dbo.Pictures", "Format", c => c.String());
            DropColumn("dbo.Pictures", "SizeBytes");
            DropColumn("dbo.Pictures", "ThumbnailBytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "ThumbnailBytes", c => c.Binary());
            AddColumn("dbo.Pictures", "SizeBytes", c => c.Binary());
            AlterColumn("dbo.Pictures", "Format", c => c.Int(nullable: false));
            DropColumn("dbo.Pictures", "resolution");
            DropColumn("dbo.Pictures", "Bytes");
            DropColumn("dbo.Pictures", "Size");
        }
    }
}
