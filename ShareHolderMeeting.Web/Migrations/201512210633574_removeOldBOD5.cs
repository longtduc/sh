namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOldBOD5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BODVotingLines");
            AddColumn("dbo.BODVotingLines", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BODVotingLines", "Id");
            DropColumn("dbo.BODVotingLines", "BODVotingLineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BODVotingLines", "BODVotingLineId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.BODVotingLines");
            DropColumn("dbo.BODVotingLines", "Id");
            AddPrimaryKey("dbo.BODVotingLines", "BODVotingLineId");
        }
    }
}
