namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeFixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileId", c => c.Int());
            AddColumn("dbo.Files", "ImageId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "ImageId");
            DropColumn("dbo.Files", "FileId");
        }
    }
}
