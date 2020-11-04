namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVotingCardShareHolderId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            RenameColumn(table: "dbo.VotingCards", name: "ShareHolderId", newName: "ShareHolder_ShareHolderId");
            AlterColumn("dbo.VotingCards", "ShareHolder_ShareHolderId", c => c.Int());
            CreateIndex("dbo.VotingCards", "ShareHolder_ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolder_ShareHolderId", "dbo.ShareHolders", "ShareHolderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolder_ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolder_ShareHolderId" });
            AlterColumn("dbo.VotingCards", "ShareHolder_ShareHolderId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.VotingCards", name: "ShareHolder_ShareHolderId", newName: "ShareHolderId");
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
        }
    }
}
