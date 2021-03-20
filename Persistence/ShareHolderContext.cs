using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Persistence
{
    public class ShareHolderContext : DbContext, IShareHolderContext
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
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        void Application.Common.Interfaces.IShareHolderContext.SaveChanges()
        {
            SaveChanges();
        }

        public void RemoveVotingCardsAndVotingByHands(int shareHolderId)
        {
            var votingCards = VotingCards.Where(v => v.ShareHolderId == shareHolderId);
            foreach (var item in votingCards)
            {
                Entry(item).State = EntityState.Deleted;
            }

            var votingByHands = VotingByHands.Where(v => v.ShareHolderId == shareHolderId);
            foreach (var item in votingByHands)
            {
                Entry(item).State = EntityState.Deleted;
            }
        }
    }
}