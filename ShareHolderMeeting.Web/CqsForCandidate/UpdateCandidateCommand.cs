using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.CqsForCandidate
{
    public class UpdateCandidateCommand
    {
        public UpdateCandidateCommand(Candidate candidate)
        {
            Candidate = candidate;
        }

        public Candidate Candidate { get; private set; }
    }
}