namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CandidateType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LogExceptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShareHolders",
                c => new
                    {
                        ShareHolderId = c.Int(nullable: false, identity: true),
                        ShareHolderCode = c.String(),
                        Name = c.String(),
                        NumberOfShares = c.Int(nullable: false),
                        StatusAtMeeting = c.Int(nullable: false),
                        MyProperty = c.String(),
                    })
                .PrimaryKey(t => t.ShareHolderId);
            
            CreateTable(
                "dbo.VotingByHands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsVoted = c.Boolean(nullable: false),
                        ShareHolderCode = c.String(),
                        ShareHolderName = c.String(),
                        NumberOfShares = c.Int(nullable: false),
                        ShareHolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
            CreateTable(
                "dbo.VotingByHandLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Int(nullable: false),
                        StatementDesc = c.String(),
                        VotingOption = c.Int(nullable: false),
                        VotingByHandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VotingByHands", t => t.VotingByHandId, cascadeDelete: true)
                .Index(t => t.VotingByHandId);
            
            CreateTable(
                "dbo.VotingCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsVoted = c.Boolean(nullable: false),
                        IsInvalid = c.Boolean(nullable: false),
                        NumberOfCandidates = c.Int(nullable: false),
                        NumberOfShares = c.Int(nullable: false),
                        AmtAlreadyVoted = c.Int(nullable: false),
                        ShareHolderId = c.Int(nullable: false),
                        VotingCardType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
            CreateTable(
                "dbo.VotingCardLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        CandidateName = c.String(),
                        VotingAmt = c.Int(nullable: false),
                        VotingCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VotingCards", t => t.VotingCardId, cascadeDelete: true)
                .Index(t => t.VotingCardId);
            
            CreateTable(
                "dbo.Statements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards");
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.VotingByHandLines", "VotingByHandId", "dbo.VotingByHands");
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.CandidateFiles", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCardId" });
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.VotingByHandLines", new[] { "VotingByHandId" });
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            DropIndex("dbo.CandidateFiles", new[] { "CandidateId" });
            DropTable("dbo.Statements");
            DropTable("dbo.VotingCardLines");
            DropTable("dbo.VotingCards");
            DropTable("dbo.VotingByHandLines");
            DropTable("dbo.VotingByHands");
            DropTable("dbo.ShareHolders");
            DropTable("dbo.LogExceptions");
            DropTable("dbo.Candidates");
            DropTable("dbo.CandidateFiles");
        }
    }
}
