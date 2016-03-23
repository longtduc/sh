namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBOSCandidate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BODCandidates", "TotalVotingAmt", c => c.Int());
            AlterColumn("dbo.BOSCandidates", "TotalVotingAmt", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BOSCandidates", "TotalVotingAmt", c => c.Int(nullable: false));
            AlterColumn("dbo.BODCandidates", "TotalVotingAmt", c => c.Int(nullable: false));
        }
    }
}
