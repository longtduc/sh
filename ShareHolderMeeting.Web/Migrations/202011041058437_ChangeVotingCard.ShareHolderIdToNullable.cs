namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVotingCardShareHolderIdToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingCards", "ShareHolderId", c => c.Int());
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            AlterColumn("dbo.VotingCards", "ShareHolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
        }
    }
}
