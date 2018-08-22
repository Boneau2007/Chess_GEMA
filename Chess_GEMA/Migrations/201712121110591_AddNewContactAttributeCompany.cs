namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewContactAttributeCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Company");
        }
    }
}
