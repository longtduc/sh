namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVotingByHand2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingByHandLines", "VotingByHand_Id", "dbo.VotingByHands");
            DropIndex("dbo.VotingByHandLines", new[] { "VotingByHand_Id" });
            RenameColumn(table: "dbo.VotingByHandLines", name: "VotingByHand_Id", newName: "VotingByHandId");
            AlterColumn("dbo.VotingByHandLines", "VotingByHandId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingByHandLines", "VotingByHandId");
            AddForeignKey("dbo.VotingByHandLines", "VotingByHandId", "dbo.VotingByHands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingByHandLines", "VotingByHandId", "dbo.VotingByHands");
            DropIndex("dbo.VotingByHandLines", new[] { "VotingByHandId" });
            AlterColumn("dbo.VotingByHandLines", "VotingByHandId", c => c.Int());
            RenameColumn(table: "dbo.VotingByHandLines", name: "VotingByHandId", newName: "VotingByHand_Id");
            CreateIndex("dbo.VotingByHandLines", "VotingByHand_Id");
            AddForeignKey("dbo.VotingByHandLines", "VotingByHand_Id", "dbo.VotingByHands", "Id");
        }
    }
}
