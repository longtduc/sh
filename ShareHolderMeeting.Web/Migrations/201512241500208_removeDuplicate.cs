namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDuplicate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BODVotingLines", "BODCandidateId", "dbo.BODCandidates");
            DropForeignKey("dbo.BODVotingLines", "BODVotingCardId", "dbo.BODVotingCards");
            DropForeignKey("dbo.BODVotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.BOSVotingCardLines", "BOSCandidateId", "dbo.BOSCandidates");
            DropForeignKey("dbo.BOSVotingCardLines", "BOSVotingCardId", "dbo.BOSVotingCards");
            DropForeignKey("dbo.BOSVotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.BODVotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.BODVotingLines", new[] { "BODCandidateId" });
            DropIndex("dbo.BODVotingLines", new[] { "BODVotingCardId" });
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSCandidateId" });
            DropIndex("dbo.BOSVotingCardLines", new[] { "BOSVotingCardId" });
            DropIndex("dbo.BOSVotingCards", new[] { "ShareHolderId" });
            DropTable("dbo.BODCandidates");
            DropTable("dbo.BODVotingCards");
            DropTable("dbo.BODVotingLines");
            DropTable("dbo.BOSCandidates");
            DropTable("dbo.BOSVotingCardLines");
            DropTable("dbo.BOSVotingCards");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.BOSVotingCardId);
            
            CreateTable(
                "dbo.BOSVotingCardLines",
                c => new
                    {
                        BOSVotingCardLineId = c.Int(nullable: false, identity: true),
                        BOSCandidateId = c.Int(nullable: false),
                        BOSVotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BOSVotingCardLineId);
            
            CreateTable(
                "dbo.BOSCandidates",
                c => new
                    {
                        BOSCandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalVotingAmt = c.Int(),
                    })
                .PrimaryKey(t => t.BOSCandidateId);
            
            CreateTable(
                "dbo.BODVotingLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BODCandidateId = c.Int(nullable: false),
                        BODVotingCardId = c.Int(nullable: false),
                        VotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BODVotingCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShareHolderId = c.Int(nullable: false),
                        NumberOfCandidates = c.Int(nullable: false),
                        NumberOfShares = c.Int(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        IsVoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BODCandidates",
                c => new
                    {
                        BODCandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalVotingAmt = c.Int(),
                    })
                .PrimaryKey(t => t.BODCandidateId);
            
            CreateIndex("dbo.BOSVotingCards", "ShareHolderId");
            CreateIndex("dbo.BOSVotingCardLines", "BOSVotingCardId");
            CreateIndex("dbo.BOSVotingCardLines", "BOSCandidateId");
            CreateIndex("dbo.BODVotingLines", "BODVotingCardId");
            CreateIndex("dbo.BODVotingLines", "BODCandidateId");
            CreateIndex("dbo.BODVotingCards", "ShareHolderId");
            AddForeignKey("dbo.BOSVotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
            AddForeignKey("dbo.BOSVotingCardLines", "BOSVotingCardId", "dbo.BOSVotingCards", "BOSVotingCardId", cascadeDelete: true);
            AddForeignKey("dbo.BOSVotingCardLines", "BOSCandidateId", "dbo.BOSCandidates", "BOSCandidateId", cascadeDelete: true);
            AddForeignKey("dbo.BODVotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
            AddForeignKey("dbo.BODVotingLines", "BODVotingCardId", "dbo.BODVotingCards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BODVotingLines", "BODCandidateId", "dbo.BODCandidates", "BODCandidateId", cascadeDelete: true);
        }
    }
}
