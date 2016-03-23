namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOldBOD3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VotingCards", newName: "BODVotingCards");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BODVotingCards", newName: "VotingCards");
        }
    }
}
