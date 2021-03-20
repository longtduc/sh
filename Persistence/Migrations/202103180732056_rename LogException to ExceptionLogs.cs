namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameLogExceptiontoExceptionLogs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LogExceptions", newName: "ExceptionLogs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ExceptionLogs", newName: "LogExceptions");
        }
    }
}
