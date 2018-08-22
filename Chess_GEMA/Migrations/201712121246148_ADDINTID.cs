namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDINTID : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Contacts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Contacts");
            AlterColumn("dbo.Contacts", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Contacts", "Id");
        }
    }
}
