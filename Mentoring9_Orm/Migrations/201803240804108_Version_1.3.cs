namespace Mentoring9_Orm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_13 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Territories", name: "RegionID", newName: "RegionsID");
            RenameIndex(table: "dbo.Territories", name: "IX_RegionID", newName: "IX_RegionsID");
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionsID = c.Int(nullable: false),
                        RegionDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.RegionsID);
            
            AddColumn("dbo.Employees", "FoundationDate", c => c.DateTime());
            DropTable("dbo.Region");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionID = c.Int(nullable: false),
                        RegionDescription = c.String(nullable: false, maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.RegionID);
            
            DropColumn("dbo.Employees", "FoundationDate");
            DropTable("dbo.Regions");
            RenameIndex(table: "dbo.Territories", name: "IX_RegionsID", newName: "IX_RegionID");
            RenameColumn(table: "dbo.Territories", name: "RegionsID", newName: "RegionID");
        }
    }
}
