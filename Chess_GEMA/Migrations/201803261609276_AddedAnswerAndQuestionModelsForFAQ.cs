namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswerAndQuestionModelsForFAQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PositivRating = c.Int(nullable: false),
                        NegativeRating = c.Int(nullable: false),
                        StarAnswer = c.Int(nullable: false),
                        Answerer_Id = c.String(maxLength: 128),
                        ReferenzQuestion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Answerer_Id)
                .ForeignKey("dbo.Questions", t => t.ReferenzQuestion_Id)
                .Index(t => t.Answerer_Id)
                .Index(t => t.ReferenzQuestion_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(),
                        Text = c.String(),
                        Likes = c.Int(nullable: false),
                        Questioner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Questioner_Id)
                .Index(t => t.Questioner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "ReferenzQuestion_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Questioner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "Questioner_Id" });
            DropIndex("dbo.Answers", new[] { "ReferenzQuestion_Id" });
            DropIndex("dbo.Answers", new[] { "Answerer_Id" });
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
