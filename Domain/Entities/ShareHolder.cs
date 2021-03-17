using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class ShareHolder
    {
        public int ShareHolderId { get; set; }
        public string ShareHolderCode {get;set;}
        public string Name {get;set;}       
        public int NumberOfShares {get;set;}
        public StatusAtMeeting StatusAtMeeting { get; set; }        
        public virtual ICollection<VotingCard> VotingCards { get; private set; }
        public virtual ICollection<VotingByHand> VotingByHands { get; set; }

        public ShareHolder()
        {
            VotingCards = new List<VotingCard>();
            VotingByHands = new List<VotingByHand>();
        }      

        public void CreateVotingCards(List<Candidate> candidates)
        {
            if (candidates == null)
                throw new ArgumentNullException("Candidates is null");

            VotingCards.Clear();

            var bodCandidates = candidates.Where(c => c.CandidateType == CandidateType.BODCandidate).ToList();
            VotingCards.Add(new VotingCard(this, bodCandidates, VotingCardType.BODVotingCard));

            var bosCandidates = candidates.Where(c => c.CandidateType == CandidateType.BOSCandidate).ToList();
            VotingCards.Add(new VotingCard(this, bosCandidates, VotingCardType.BOSVotingCard));

        }

        public void CreateVotingByHands(List<Statement> statements)
        {
            if (statements == null)
                throw new ArgumentNullException();
            VotingByHands.Add(new VotingByHand(this,statements));            
        }
       
    }

    public enum StatusAtMeeting
    {
        Absent = 0,
        Attended = 1,
        Delegated = 2
    }
}