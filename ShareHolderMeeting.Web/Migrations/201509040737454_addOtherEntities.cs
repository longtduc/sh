namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOtherEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BODCandidates",
                c => new
                    {
                        BODCandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalVotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BODCandidateId);
            
            CreateTable(
                "dbo.BODVotingCardLines",
                c => new
                    {
                        BODVotingCardLineId = c.Int(nullable: false, identity: true),
                        BODCandidateId = c.Int(nullable: false),
                        BODVotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BODVotingCardLineId)
                .ForeignKey("dbo.BODCandidates", t => t.BODCandidateId, cascadeDelete: true)
                .ForeignKey("dbo.BODVotingCards", t => t.BODVotingCardId, cascadeDelete: true)
                .Index(t => t.BODCandidateId)
                .Index(t => t.BODVotingCardId);
            
            CreateTable(
                "dbo.BODVotingCards",
                c => new
                    {
                        BODVotingCardId = c.Int(nullable: false, identity: true),
                        ShareHolderId = c.Int(nullable: false),
                        ShareHolderCode = c.String(),
                        ShareHolderName = c.String(),
                        NumberOfShares = c.Int(nullable: false),
                        NumberOfCandidates = c.Int(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        IsVoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BODVotingCardId)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
            CreateTable(
                "dbo.ShareHolders",
                c => new
                    {
                        ShareHolderId = c.Int(nullable: false, identity: true),
                        ShareHolderCode = c.String(),
                        Name = c.String(),
                        NumberOfShares = c.Int(nullable: false),
                        StatusAtMeeting = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShareHolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BODVotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.BODVotingCardLines", "BODVotingCardId", "dbo.BODVotingCards");
            DropForeignKey("dbo.BODVotingCardLines", "BODCandidateId", "dbo.BODCandidates");
            DropIndex("dbo.BODVotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.BODVotingCardLines", new[] { "BODVotingCardId" });
            DropIndex("dbo.BODVotingCardLines", new[] { "BODCandidateId" });
            DropTable("dbo.ShareHolders");
            DropTable("dbo.BODVotingCards");
            DropTable("dbo.BODVotingCardLines");
            DropTable("dbo.BODCandidates");
        }
    }
}
