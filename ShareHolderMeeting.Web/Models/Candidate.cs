using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {

            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Candidate Name must be not empty!");

                name = value;
            }
        }

        public CandidateType CandidateType { get; set; }
        public bool IsValidForVotingCard(VotingCardType type)
        {
            if (type == VotingCardType.BODVotingCard)
                return (this.CandidateType == CandidateType.BODCandidate);
            else
                return (this.CandidateType == CandidateType.BOSCandidate);
        }
    }


    public enum CandidateType
    {
        BODCandidate = 1,
        BOSCandidate = 2
    }

}