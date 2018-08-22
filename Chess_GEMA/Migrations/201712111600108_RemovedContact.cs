namespace Chess_GEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedContact : DbMigration
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
    }
}
