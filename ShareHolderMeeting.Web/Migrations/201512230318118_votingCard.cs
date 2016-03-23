namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votingCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotingCardLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        CandidateName = c.String(),
                        VotingAmt = c.Int(nullable: false),
                        VotingCardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VotingCards", t => t.VotingCardId, cascadeDelete: true)
                .Index(t => t.VotingCardId);
            
            CreateTable(
                "dbo.VotingCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsVoted = c.Boolean(nullable: false),
                        VotingCardType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingCardLines", "VotingCardId", "dbo.VotingCards");
            DropIndex("dbo.VotingCardLines", new[] { "VotingCardId" });
            DropTable("dbo.VotingCards");
            DropTable("dbo.VotingCardLines");
        }
    }
}
