namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPropsSubscriber : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Questioner_Id", "dbo.AspNetUsers");
            AddColumn("dbo.AspNetUsers", "Question_Id", c => c.Int());
            AddColumn("dbo.Questions", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Question_Id");
            CreateIndex("dbo.Questions", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Question_Id", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Question_Id" });
            DropColumn("dbo.Questions", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Question_Id");
            AddForeignKey("dbo.Questions", "Questioner_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
