namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFilesTable2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Files", "CandidateId");
            AddForeignKey("dbo.Files", "CandidateId", "dbo.Candidates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Files", new[] { "CandidateId" });
        }
    }
}
