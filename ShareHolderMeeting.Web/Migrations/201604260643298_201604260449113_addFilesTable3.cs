namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201604260449113_addFilesTable3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "CandidateFiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CandidateFiles", newName: "Files");
        }
    }
}
