namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedVotingCardLinesVotingCardId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCardId" });
            RenameColumn(table: "dbo.VotingCardLines", name: "VotingCardId", newName: "VotingCard_Id");
            AlterColumn("dbo.VotingCardLines", "VotingCard_Id", c => c.Int());
            CreateIndex("dbo.VotingCardLines", "VotingCard_Id");
            AddForeignKey("dbo.VotingCardLines", "VotingCard_Id", "dbo.VotingCards", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCard_Id", "dbo.VotingCards");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCard_Id" });
            AlterColumn("dbo.VotingCardLines", "VotingCard_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.VotingCardLines", name: "VotingCard_Id", newName: "VotingCardId");
            CreateIndex("dbo.VotingCardLines", "VotingCardId");
            AddForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards", "Id", cascadeDelete: true);
        }
    }
}
