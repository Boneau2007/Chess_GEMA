namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequirementsToQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Gallery_Id", c => c.Int());
            AlterColumn("dbo.Answers", "Text", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Questions", "Topic", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Questions", "Text", c => c.String(maxLength: 513));
            CreateIndex("dbo.Files", "Gallery_Id");
            AddForeignKey("dbo.Files", "Gallery_Id", "dbo.Galleries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.Files", new[] { "Gallery_Id" });
            AlterColumn("dbo.Questions", "Text", c => c.String());
            AlterColumn("dbo.Questions", "Topic", c => c.String());
            AlterColumn("dbo.Answers", "Text", c => c.String());
            DropColumn("dbo.Files", "Gallery_Id");
        }
    }
}
