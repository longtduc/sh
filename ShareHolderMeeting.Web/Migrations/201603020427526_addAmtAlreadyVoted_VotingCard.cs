namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAmtAlreadyVoted_VotingCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingCards", "AmtAlreadyVoted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VotingCards", "AmtAlreadyVoted");
        }
    }
}
