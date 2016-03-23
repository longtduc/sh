namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmployee2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FTEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AnnualSalary = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.Employees", "AnnualSalary");
            DropColumn("dbo.Employees", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Employees", "AnnualSalary", c => c.Int());
            DropForeignKey("dbo.FTEmployees", "Id", "dbo.Employees");
            DropIndex("dbo.FTEmployees", new[] { "Id" });
            DropTable("dbo.FTEmployees");
        }
    }
}
