namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFileIdAndImageId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "FileId");
            DropColumn("dbo.Files", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "ImageId", c => c.Int());
            AddColumn("dbo.Files", "FileId", c => c.Int());
        }
    }
}
