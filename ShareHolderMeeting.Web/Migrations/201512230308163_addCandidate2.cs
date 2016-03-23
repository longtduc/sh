namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCandidate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "CandidateType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "CandidateType");
        }
    }
}
