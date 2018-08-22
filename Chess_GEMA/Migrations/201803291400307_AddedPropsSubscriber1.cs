namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropsSubscriber1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Questions", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "ApplicationUser_Id");
            AddForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
