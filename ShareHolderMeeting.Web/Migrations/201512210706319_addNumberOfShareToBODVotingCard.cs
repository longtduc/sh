namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberOfShareToBODVotingCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BODVotingCards", "NumberOfShares", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BODVotingCards", "NumberOfShares");
        }
    }
}
