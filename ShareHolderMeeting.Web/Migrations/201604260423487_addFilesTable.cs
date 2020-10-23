namespace ShareHolderMeeting.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFilesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Candidates", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "Image", c => c.Binary());
            DropTable("dbo.Files");
        }
    }
}
