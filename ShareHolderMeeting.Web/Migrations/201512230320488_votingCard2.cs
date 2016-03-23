namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votingCard2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingCards", "IsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.VotingCards", "ShareHolderId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingCards", "ShareHolderId");
            AddForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders", "ShareHolderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCards", "ShareHolderId", "dbo.ShareHolders");
            DropIndex("dbo.VotingCards", new[] { "ShareHolderId" });
            DropColumn("dbo.VotingCards", "ShareHolderId");
            DropColumn("dbo.VotingCards", "IsValid");
        }
    }
}
