namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PictureTableNameGeändert : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PictureModels", newName: "Pictures");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Pictures", newName: "PictureModels");
        }
    }
}
