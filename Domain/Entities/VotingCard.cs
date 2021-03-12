using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class VotingCard
    {
        public int Id { get; set; }
        public bool IsVoted { get; set; }
        public bool IsInvalid { get; set; }
        public int NumberOfCandidates { get; set; }

        public int NumberOfShares { get; set; }
        public int AmtAlreadyVoted { get; set; }
        public ShareHolder ShareHolder { get; set; }
        public int ShareHolderId { get; set; }
        public VotingCardType VotingCardType { get; set; }
        public ICollection<VotingCardLine> VotingCardLines { get; set; }

        public VotingCard()
        {
            VotingCardLines = new List<VotingCardLine>();
        }
        public VotingCard(ShareHolder shareHolder, List<Candidate> candidates, VotingCardType type) : this()
        {
            if (shareHolder.StatusAtMeeting == StatusAtMeeting.Absent)
                throw new InvalidOperationException("Could not create VotingCard for absent shareholder");

            if (candidates == null)
                throw new ArgumentNullException("");

            this.ShareHolderId = shareHolder.ShareHolderId;
            this.VotingCardType = type;
            foreach (var candidate in candidates)
            {
                if (candidate.CanBeVoted(type))
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

        public void RevertVoting()
        {
            IsInvalid = false;
            IsVoted = false;
            foreach (var item in VotingCardLines)
            {
                item.VotingAmt = 0;
            }
            SetAmtAlreadyVoted();
        }

        public void Vote(bool isInvalid, IList<VotingCardLine> votingCardLines)
        {
            if (isInvalid)
            {
                IsVoted = false;
                IsInvalid = true;
                foreach (var item in VotingCardLines)
                {
                    item.VotingAmt = 0;
                }
                SetAmtAlreadyVoted();
                return;
            }

            foreach (var item in VotingCardLines)
            {
                var newItem = votingCardLines.Where(n => n.Id == item.Id).FirstOrDefault();
                if (newItem == null)
                    throw new InvalidOperationException();
                item.VotingAmt = newItem.VotingAmt;
            }

            IsVoted = true;
            SetAmtAlreadyVoted();
        }

        private void SetAmtAlreadyVoted()
        {
            AmtAlreadyVoted = 0;
            if (IsInvalid || !IsVoted)
            {
                AmtAlreadyVoted = 0;
                return;
            }
            foreach (var line in VotingCardLines)
            {
                AmtAlreadyVoted += line.VotingAmt;
            }

        }

        public void RemoveVotingCardLines()
        {
            foreach (var item in VotingCardLines.ToList())
            {
                VotingCardLines.Remove(item);
            }            
        }

    }

    public enum VotingCardType
    {
        BODVotingCard = 1,
        BOSVotingCard = 2
    }
}