namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Title", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "Title");
        }
    }
}
