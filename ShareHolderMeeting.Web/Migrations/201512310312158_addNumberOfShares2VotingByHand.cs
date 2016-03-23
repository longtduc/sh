namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfShares2VotingByHand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingByHands", "NumberOfShares", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VotingByHands", "NumberOfShares");
        }
    }
}
