namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOldBOD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BODVotingCardLines", "BODCandidateId", "dbo.BODCandidates");
            DropForeignKey("dbo.BODVotingCardLines", "BODVotingCardId", "dbo.BODVotingCards");
            DropForeignKey("dbo.BODVotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.BODVotingCardLines", new[] { "BODCandidateId" });
            DropIndex("dbo.BODVotingCardLines", new[] { "BODVotingCardId" });
            DropIndex("dbo.BODVotingCards", new[] { "ShareHolderId" });
            DropTable("dbo.BODVotingCardLines");
            DropTable("dbo.BODVotingCards");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.BODVotingCardId);
            
            CreateTable(
                "dbo.BODVotingCardLines",
                c => new
                    {
                        BODVotingCardLineId = c.Int(nullable: false, identity: true),
                        BODCandidateId = c.Int(nullable: false),
                        BODVotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BODVotingCardLineId);
            
            CreateIndex("dbo.BODVotingCards", "ShareHolderId");
            CreateIndex("dbo.BODVotingCardLines", "BODVotingCardId");
            CreateIndex("dbo.BODVotingCardLines", "BODCandidateId");
            AddForeignKey("dbo.BODVotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
            AddForeignKey("dbo.BODVotingCardLines", "BODVotingCardId", "dbo.BODVotingCards", "BODVotingCardId", cascadeDelete: true);
            AddForeignKey("dbo.BODVotingCardLines", "BODCandidateId", "dbo.BODCandidates", "BODCandidateId", cascadeDelete: true);
        }
    }
}
