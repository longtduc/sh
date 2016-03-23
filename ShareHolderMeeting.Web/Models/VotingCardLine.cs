using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class VotingCardLine
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public int VotingAmt { get; set; }
        public int VotingCardId { get; set; }
        public VotingCard VotingCard { get; set; }
    }
}