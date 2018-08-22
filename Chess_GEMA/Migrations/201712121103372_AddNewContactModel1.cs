namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewContactModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Titel = c.String(),
                        Sex = c.String(),
                        SurName = c.String(),
                        FirstName = c.String(),
                        Street = c.String(),
                        StreetNumber = c.String(),
                        Postcode = c.Int(nullable: false),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
