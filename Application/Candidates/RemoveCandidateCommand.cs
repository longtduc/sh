using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Candidates
{
    public class RemoveCandidateCommand
    {
        public int CandidateId { get; }

        public RemoveCandidateCommand(int candidateId)
        {
            CandidateId = candidateId;
        }
    }
}