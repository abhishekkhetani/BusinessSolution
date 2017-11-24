namespace BusinessSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        City = c.String(),
                        Phone = c.String(),
                        Rupees = c.String(),
                        PaymentStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentDetails");
        }
    }
}
