namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPropStarAnswer1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ReferenzQuestion_Id", "dbo.Questions");
            AddColumn("dbo.Answers", "Question_Id", c => c.Int());
            AddColumn("dbo.Questions", "StarAnswer_Id", c => c.Int());
            CreateIndex("dbo.Answers", "Question_Id");
            CreateIndex("dbo.Questions", "StarAnswer_Id");
            AddForeignKey("dbo.Questions", "StarAnswer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.Answers", "Question_Id", "dbo.Questions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "StarAnswer_Id", "dbo.Answers");
            DropIndex("dbo.Questions", new[] { "StarAnswer_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropColumn("dbo.Questions", "StarAnswer_Id");
            DropColumn("dbo.Answers", "Question_Id");
            AddForeignKey("dbo.Answers", "ReferenzQuestion_Id", "dbo.Questions", "Id");
        }
    }
}
