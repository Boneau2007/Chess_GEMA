namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPropsWithRestrictions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Content", c => c.String());
            AlterColumn("dbo.Questions", "Text", c => c.String(maxLength: 512));
            AlterColumn("dbo.Files", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Files", "Size", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "Size", c => c.Int(nullable: false));
            AlterColumn("dbo.Files", "Name", c => c.String());
            AlterColumn("dbo.Questions", "Text", c => c.String(maxLength: 513));
            DropColumn("dbo.Files", "Content");
        }
    }
}
