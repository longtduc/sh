namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBOSCandidate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOSCandidates",
                c => new
                    {
                        BOSCandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TotalVotingAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BOSCandidateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BOSCandidates");
        }
    }
}
