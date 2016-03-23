namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeEmp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FTEmployees", "Id", "dbo.Employees");
            DropIndex("dbo.FTEmployees", new[] { "Id" });
            DropTable("dbo.Employees");
            DropTable("dbo.FTEmployees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FTEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AnnualSalary = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FTEmployees", "Id");
            AddForeignKey("dbo.FTEmployees", "Id", "dbo.Employees", "Id");
        }
    }
}
