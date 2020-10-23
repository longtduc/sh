namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageToCandidate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "Image");
        }
    }
}
