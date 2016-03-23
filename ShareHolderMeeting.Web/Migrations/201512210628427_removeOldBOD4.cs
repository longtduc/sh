namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOldBOD4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BODVotingLines", name: "VotingCardId", newName: "BODVotingCardId");
            RenameIndex(table: "dbo.BODVotingLines", name: "IX_VotingCardId", newName: "IX_BODVotingCardId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BODVotingLines", name: "IX_BODVotingCardId", newName: "IX_VotingCardId");
            RenameColumn(table: "dbo.BODVotingLines", name: "BODVotingCardId", newName: "VotingCardId");
        }
    }
}
