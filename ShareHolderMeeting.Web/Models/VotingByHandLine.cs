using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class VotingByHandLine
    {
        public int Id { get; set; }
        public int StatementId { get; set; }
        public string StatementDesc { get; set; }
        public VotingOption VotingOption { get; set; }
        public int VotingByHandId { get; set; }
        public VotingByHand VotingByHand { get; set; }

    }

    public enum VotingOption 
    { 
        Yes =0,
        No =1,
        Other =2
    }
}