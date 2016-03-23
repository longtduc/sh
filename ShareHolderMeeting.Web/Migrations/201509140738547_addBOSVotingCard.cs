namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBOSVotingCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOSVotingCardLines",
                c => new
                    {
                        BOSVotingCardLineId = c.Int(nullable: false, identity: true),
                        BODCandidateId = c.Int(nullable: false),
                        BOSVotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                        BOSCandidate_BOSCandidateId = c.Int(),
                    })
                .PrimaryKey(t => t.BOSVotingCardLineId)
                .ForeignKey("dbo.BOSCandidates", t => t.BOSCandidate_BOSCandidateId)
                .ForeignKey("dbo.BOSVotingCards", t => t.BOSVotingCardId, cascadeDelete: true)
                .Index(t => t.BOSVotingCardId)
                .Index(t => t.BOSCandidate_BOSCandidateId);
            
            CreateTable(
                "dbo.BOSVotingCards",
                c => new
                    {
                        BOSVotingCardId = c.Int(nullable: false, identity: true),
                        ShareHolderId = c.Int(nullable: false),
                        ShareHolderCode = c.String(),
                        ShareHolderName = c.String(),
                        NumberOfShares = c.Int(nullable: false),
                        NumberOfCandidates = c.Int(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        IsVoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BOSVotingCardId)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BOSVotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.BOSVotingCardLines", "BOSVotingCardId", "dbo.BOSVotingCards");
            DropForeignKey("dbo.BOSVotingCardLines", "BOSCandidate_BOSCandidateId", "dbo.BOSCandidates");
            DropIndex("dbo.BOSVotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSCandidate_BOSCandidateId" });
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSVotingCardId" });
            DropTable("dbo.BOSVotingCards");
            DropTable("dbo.BOSVotingCardLines");
        }
    }
}
