namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContactToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Sex", c => c.String());
            AddColumn("dbo.AspNetUsers", "SurName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Street", c => c.String());
            AddColumn("dbo.AspNetUsers", "StreetNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Postcode", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Company");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Postcode");
            DropColumn("dbo.AspNetUsers", "StreetNumber");
            DropColumn("dbo.AspNetUsers", "Street");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "SurName");
            DropColumn("dbo.AspNetUsers", "Sex");
        }
    }
}
