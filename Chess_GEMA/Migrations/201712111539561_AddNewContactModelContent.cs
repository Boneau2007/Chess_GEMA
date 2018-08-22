namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewContactModelContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CeoName = c.String(),
                        CompanyName = c.String(),
                        Street = c.String(),
                        Streetnumber = c.String(),
                        PostCode = c.String(),
                        Village = c.String(),
                        Phonenumber = c.String(),
                        Fax = c.String(),
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
