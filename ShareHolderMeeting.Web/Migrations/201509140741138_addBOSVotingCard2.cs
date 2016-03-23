namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBOSVotingCard2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BOSVotingCardLines", "BOSCandidate_BOSCandidateId", "dbo.BOSCandidates");
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSCandidate_BOSCandidateId" });
            RenameColumn(table: "dbo.BOSVotingCardLines", name: "BOSCandidate_BOSCandidateId", newName: "BOSCandidateId");
            AlterColumn("dbo.BOSVotingCardLines", "BOSCandidateId", c => c.Int(nullable: false));
            CreateIndex("dbo.BOSVotingCardLines", "BOSCandidateId");
            AddForeignKey("dbo.BOSVotingCardLines", "BOSCandidateId", "dbo.BOSCandidates", "BOSCandidateId", cascadeDelete: true);
            DropColumn("dbo.BOSVotingCardLines", "BODCandidateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BOSVotingCardLines", "BODCandidateId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BOSVotingCardLines", "BOSCandidateId", "dbo.BOSCandidates");
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSCandidateId" });
            AlterColumn("dbo.BOSVotingCardLines", "BOSCandidateId", c => c.Int());
            RenameColumn(table: "dbo.BOSVotingCardLines", name: "BOSCandidateId", newName: "BOSCandidate_BOSCandidateId");
            CreateIndex("dbo.BOSVotingCardLines", "BOSCandidate_BOSCandidateId");
            AddForeignKey("dbo.BOSVotingCardLines", "BOSCandidate_BOSCandidateId", "dbo.BOSCandidates", "BOSCandidateId");
        }
    }
}
