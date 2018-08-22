namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewContactRestrictions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "SurName", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "FirstName", c => c.String());
            AlterColumn("dbo.Contacts", "SurName", c => c.String());
        }
    }
}
