namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "MyProperty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "MyProperty");
        }
    }
}
