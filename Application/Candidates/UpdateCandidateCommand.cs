using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Candidates
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