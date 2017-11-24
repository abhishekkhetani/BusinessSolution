namespace BusinessSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abhishek : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.paymentdetails", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.paymentdetails", "MiddleName", c => c.String(nullable: false));
            AlterColumn("dbo.paymentdetails", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.paymentdetails", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.paymentdetails", "City", c => c.String(nullable: false));
            AlterColumn("dbo.paymentdetails", "Phone", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.paymentdetails", "Rupees", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.paymentdetails", "Rupees", c => c.String());
            AlterColumn("dbo.paymentdetails", "Phone", c => c.String());
            AlterColumn("dbo.paymentdetails", "City", c => c.String());
            AlterColumn("dbo.paymentdetails", "Description", c => c.String());
            AlterColumn("dbo.paymentdetails", "LastName", c => c.String());
            AlterColumn("dbo.paymentdetails", "MiddleName", c => c.String());
            AlterColumn("dbo.paymentdetails", "FirstName", c => c.String());
        }
    }
}
