namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedVotingByHandShareHolderIdToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingByHands", "ShareHolderId", c => c.Int());
            CreateIndex("dbo.VotingByHands", "ShareHolderId");
            AddForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingByHands", "ShareHolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingByHands", "ShareHolderId");
            AddForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
        }
    }
}
