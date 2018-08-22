namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedModelVarialeTitel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.NewsFeeds", name: "TitalPicture_Id", newName: "TitelPicture_Id");
            RenameIndex(table: "dbo.NewsFeeds", name: "IX_TitalPicture_Id", newName: "IX_TitelPicture_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.NewsFeeds", name: "IX_TitelPicture_Id", newName: "IX_TitalPicture_Id");
            RenameColumn(table: "dbo.NewsFeeds", name: "TitelPicture_Id", newName: "TitalPicture_Id");
        }
    }
}
