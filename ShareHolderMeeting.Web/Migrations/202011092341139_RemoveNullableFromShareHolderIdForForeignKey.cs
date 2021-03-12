namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNullableFromShareHolderIdForForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingByHands", "ShareHolderId", c => c.Int(nullable: false));
            AlterColumn("dbo.VotingCards", "ShareHolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingByHands", "ShareHolderId");
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            AddForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
            DropColumn("dbo.Candidates", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "MyProperty", c => c.Int(nullable: false));
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            DropIndex("dbo.VotingByHands", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingCards", "ShareHolderId", c => c.Int());
            AlterColumn("dbo.VotingByHands", "ShareHolderId", c => c.Int());
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            CreateIndex("dbo.VotingByHands", "ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId");
            AddForeignKey("dbo.VotingByHands", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId");
        }
    }
}
