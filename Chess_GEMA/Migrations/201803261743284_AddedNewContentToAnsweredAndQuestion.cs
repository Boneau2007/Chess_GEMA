namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewContentToAnsweredAndQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "Answered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Answered");
            DropColumn("dbo.Questions", "Date");
            DropColumn("dbo.Answers", "Date");
        }
    }
}
