namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNullabeToNotNullForVotingCardLineVotingCardId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCardId" });
            AlterColumn("dbo.VotingCardLines", "VotingCardId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingCardLines", "VotingCardId");
            AddForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCardId" });
            AlterColumn("dbo.VotingCardLines", "VotingCardId", c => c.Int());
            CreateIndex("dbo.VotingCardLines", "VotingCardId");
            AddForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards", "Id");
        }
    }
}
