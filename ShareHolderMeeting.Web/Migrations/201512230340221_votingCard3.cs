namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votingCard3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingCards", "NumberOfCandidates", c => c.Int(nullable: false));
            AddColumn("dbo.VotingCards", "NumberOfShares", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VotingCards", "NumberOfShares");
            DropColumn("dbo.VotingCards", "NumberOfCandidates");
        }
    }
}
