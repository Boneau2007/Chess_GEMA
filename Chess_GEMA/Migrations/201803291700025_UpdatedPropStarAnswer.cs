namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPropStarAnswer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "StarAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "StarAnswer", c => c.Int(nullable: false));
        }
    }
}
