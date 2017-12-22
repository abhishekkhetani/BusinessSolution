namespace BusinessSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.paymentdetails", "Phone", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.paymentdetails", "Phone", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
