using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class ShareHolderContext : DbContext
    {
        public ShareHolderContext()
            : base("name=ShareHolderMeeting")
        {

        }
        public DbSet<ShareHolder> ShareHolders { get; set; }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateFile> CandidateFiles { get; set; }
        public DbSet<VotingCard> VotingCards { get; set; }
        public DbSet<VotingCardLine> VotingCardLines { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<VotingByHand> VotingByHands { get; set; }
        public DbSet<VotingByHandLine> VotingByHandLines { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            this.Configuration.ProxyCreationEnabled = false;
        }

    }
}