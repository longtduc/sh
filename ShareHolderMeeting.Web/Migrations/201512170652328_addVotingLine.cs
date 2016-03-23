namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVotingLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BODVotingLines",
                c => new
                    {
                        BODVotingLineId = c.Int(nullable: false, identity: true),
                        BODCandidateId = c.Int(nullable: false),
                        VotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BODVotingLineId)
                .ForeignKey("dbo.BODCandidates", t => t.BODCandidateId, cascadeDelete: true)
                .ForeignKey("dbo.VotingCards", t => t.VotingCardId, cascadeDelete: true)
                .Index(t => t.BODCandidateId)
                .Index(t => t.VotingCardId);
            
            CreateTable(
                "dbo.VotingCards",
                c => new
                    {
                        VotingCardId = c.Int(nullable: false, identity: true),
                        ShareHolderId = c.Int(nullable: false),
                        numberOfCandidates = c.Int(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        IsVoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VotingCardId)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.BODVotingLines", "VotingCardId", "dbo.VotingCards");
            DropForeignKey("dbo.BODVotingLines", "BODCandidateId", "dbo.BODCandidates");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.BODVotingLines", new[] { "VotingCardId" });
            DropIndex("dbo.BODVotingLines", new[] { "BODCandidateId" });
            DropTable("dbo.VotingCards");
            DropTable("dbo.BODVotingLines");
        }
    }
}
