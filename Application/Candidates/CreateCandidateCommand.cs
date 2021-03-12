using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Candidates
{
    public class CreateCandidateCommand
    {
        public CreateCandidateCommand(Candidate candidate)
        {
            Candidate = candidate;
        }       
        public Candidate Candidate { get; private set; }
    }
}