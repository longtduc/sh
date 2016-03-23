namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVotingOption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VotingByHandLines", "VotingOption", c => c.Int(nullable: false));
            DropColumn("dbo.VotingByHandLines", "IsOK");
            DropColumn("dbo.VotingByHandLines", "IsNotOK");
            DropColumn("dbo.VotingByHandLines", "IsOtherOpinion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VotingByHandLines", "IsOtherOpinion", c => c.Boolean(nullable: false));
            AddColumn("dbo.VotingByHandLines", "IsNotOK", c => c.Boolean(nullable: false));
            AddColumn("dbo.VotingByHandLines", "IsOK", c => c.Boolean(nullable: false));
            DropColumn("dbo.VotingByHandLines", "VotingOption");
        }
    }
}
