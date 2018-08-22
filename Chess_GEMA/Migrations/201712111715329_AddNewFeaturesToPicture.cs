namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFeaturesToPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "Owner", c => c.String());
            AddColumn("dbo.Pictures", "Size", c => c.String());
            AddColumn("dbo.Pictures", "Format", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "Format");
            DropColumn("dbo.Pictures", "Size");
            DropColumn("dbo.Pictures", "Owner");
        }
    }
}
