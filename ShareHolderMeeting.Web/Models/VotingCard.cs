using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class VotingCard
    {
        public int Id { get; set; }
        public bool IsVoted { get; set; }
        public bool IsInvalid { get; set; }
        public int NumberOfCandidates { get; set; }
        public int NumberOfShares { get; set; }
        public int ShareHolderId { get; set; }
        public int AmtAlreadyVoted { get; private set; }
        public ShareHolder ShareHolder { get; set; }
        public VotingCardType VotingCardType { get; set; }
        public ICollection<VotingCardLine> VotingCardLines { get; set; }

        public VotingCard()
        {
            VotingCardLines = new List<VotingCardLine>();
        }
        public VotingCard(ShareHolder shareHolder, List<Candidate> candidates, VotingCardType type)
        {
            if (shareHolder.StatusAtMeeting == StatusAtMeeting.Absent)
                throw new ArgumentException("Could not create VotingCard for absent shareholder");
            VotingCardLines = new List<VotingCardLine>();
            this.ShareHolderId = shareHolder.ShareHolderId;
            this.VotingCardType = type;
            foreach (var candidate in candidates)
            {
                if (candidate.IsValidForVotingCard(type))
                {
                    var line = new VotingCardLine()
                    {
                        CandidateId = candidate.Id,
                        CandidateName = candidate.Name,
                    };
                    VotingCardLines.Add(line);
                }
            }
            this.NumberOfCandidates = VotingCardLines.Count();
            this.NumberOfShares = shareHolder.NumberOfShares;
        }

        public void SetAmtAlreadyVoted()
        {
            if (IsInvalid || !IsVoted)
            {
                this.AmtAlreadyVoted = 0;
                return;
            }
            foreach (var line in VotingCardLines)
            {
                this.AmtAlreadyVoted += line.VotingAmt;
            }
        }

    }



    public enum VotingCardType
    {
        BODVotingCard = 1,
        BOSVotingCard = 2
    }
}