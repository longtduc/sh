namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShareHolderIdWithNullableTypeOfInteger : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.VotingCards", name: "ShareHolder_ShareHolderId", newName: "ShareHolderId");
            RenameColumn(table: "dbo.VotingCardLines", name: "VotingCard_Id", newName: "VotingCardId");
            RenameIndex(table: "dbo.VotingCards", name: "IX_ShareHolder_ShareHolderId", newName: "IX_ShareHolderId");
            RenameIndex(table: "dbo.VotingCardLines", name: "IX_VotingCard_Id", newName: "IX_VotingCardId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.VotingCardLines", name: "IX_VotingCardId", newName: "IX_VotingCard_Id");
            RenameIndex(table: "dbo.VotingCards", name: "IX_ShareHolderId", newName: "IX_ShareHolder_ShareHolderId");
            RenameColumn(table: "dbo.VotingCardLines", name: "VotingCardId", newName: "VotingCard_Id");
            RenameColumn(table: "dbo.VotingCards", name: "ShareHolderId", newName: "ShareHolder_ShareHolderId");
        }
    }
}
