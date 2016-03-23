namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votingCard4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingCards", "IsInvalid", c => c.Boolean(nullable: false));
            DropColumn("dbo.VotingCards", "IsValid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VotingCards", "IsValid", c => c.Boolean(nullable: false));
            DropColumn("dbo.VotingCards", "IsInvalid");
        }
    }
}
