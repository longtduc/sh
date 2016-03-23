namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOldBOD2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BODVotingLines", "VotingCardId", "dbo.VotingCards");
            DropPrimaryKey("dbo.VotingCards");
            AddColumn("dbo.VotingCards", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.VotingCards", "Id");
            AddForeignKey("dbo.BODVotingLines", "VotingCardId", "dbo.VotingCards", "Id", cascadeDelete: true);
            DropColumn("dbo.VotingCards", "VotingCardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VotingCards", "VotingCardId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.BODVotingLines", "VotingCardId", "dbo.VotingCards");
            DropPrimaryKey("dbo.VotingCards");
            DropColumn("dbo.VotingCards", "Id");
            AddPrimaryKey("dbo.VotingCards", "VotingCardId");
            AddForeignKey("dbo.BODVotingLines", "VotingCardId", "dbo.VotingCards", "VotingCardId", cascadeDelete: true);
        }
    }
}
