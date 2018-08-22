namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Images", newName: "Files");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Files", newName: "Images");
        }
    }
}
