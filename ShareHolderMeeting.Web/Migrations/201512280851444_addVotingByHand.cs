namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVotingByHand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotingByHandLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementId = c.Int(nullable: false),
                        StatementDesc = c.String(),
                        IsOK = c.Boolean(nullable: false),
                        IsNotOK = c.Boolean(nullable: false),
                        IsOtherOpinion = c.Boolean(nullable: false),
                        VotingByHand_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VotingByHands", t => t.VotingByHand_Id)
                .Index(t => t.VotingByHand_Id);
            
            CreateTable(
                "dbo.VotingByHands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsVoted = c.Boolean(nullable: false),
                        ShareHolderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShareHolders", t => t.ShareHolderId, cascadeDelete: true)
                .Index(t => t.ShareHolderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingByHandLines", "VotingByHand_Id", "dbo.VotingByHands");
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            DropIndex("dbo.VotingByHandLines", new[] { "VotingByHand_Id" });
            DropTable("dbo.VotingByHands");
            DropTable("dbo.VotingByHandLines");
        }
    }
}
