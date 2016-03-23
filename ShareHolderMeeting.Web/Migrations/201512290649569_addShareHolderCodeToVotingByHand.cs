namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addShareHolderCodeToVotingByHand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingByHands", "ShareHolderCode", c => c.String());
            AddColumn("dbo.VotingByHands", "ShareHolderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VotingByHands", "ShareHolderName");
            DropColumn("dbo.VotingByHands", "ShareHolderCode");
        }
    }
}
