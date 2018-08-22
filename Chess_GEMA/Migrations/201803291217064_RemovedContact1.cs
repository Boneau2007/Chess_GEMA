namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedContact1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Contacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Sex = c.String(),
                        SurName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Company = c.String(),
                        Street = c.String(),
                        StreetNumber = c.String(),
                        Postcode = c.Int(nullable: false),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
