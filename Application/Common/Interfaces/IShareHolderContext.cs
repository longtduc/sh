using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IShareHolderContext
    {
        DbSet<ShareHolder> ShareHolders { get; set; }
        DbSet<Candidate> Candidates { get; set; }
        DbSet<CandidateFile> CandidateFiles { get; set; }
        DbSet<VotingCard> VotingCards { get; set; }
        DbSet<VotingCardLine> VotingCardLines { get; set; }
        DbSet<Statement> Statements { get; set; }
        DbSet<VotingByHand> VotingByHands { get; set; }
        DbSet<VotingByHandLine> VotingByHandLines { get; set; }
        void RemoveVotingCardsAndVotingByHands(int shareHolderId);
        void SaveChanges();
    }
}
