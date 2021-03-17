namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedMyProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShareHolders", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShareHolders", "MyProperty", c => c.String());
        }
    }
}
