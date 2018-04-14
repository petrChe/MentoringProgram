namespace Mentoring9_Orm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeCreditCardInfoes",
                c => new
                    {
                        EmployeeCreditCardInfoID = c.String(nullable: false, maxLength: 5),
                        ExpiredDate = c.DateTime(),
                        CreditCardNumber = c.String(maxLength: 10),
                        CreditCardHolderName = c.String(maxLength: 40),
                        EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeCreditCardInfoID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeCreditCardInfoes", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeCreditCardInfoes", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeCreditCardInfoes");
        }
    }
}
