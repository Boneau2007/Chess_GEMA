namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedTrouble : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pictures", "Bytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "Bytes", c => c.Binary());
        }
    }
}
