﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Common.Models
{
    public class VotingCardVM
    {
        public int Id { get; set; }
        public bool IsVoted { get; set; }
        public bool IsInvalid { get; set; }
        public int NumberOfCandidates { get; set; }

        public int NumberOfShares { get; set; }
        public int AmtAlreadyVoted { get; set; }
        public Nullable<int> ShareHolderId { get; set; }
        public VotingCardType VotingCardType { get; set; }
        public ICollection<VotingCardLineVM> VotingCardLines { get; set; }       
    }
    public class VotingCardLineVM
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public int VotingAmt { get; set; }
        public int VotingCardId { get; set; }       
    }
}

