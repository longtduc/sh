using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Candidates
{
    public class CandidateValidator : IValidator<Candidate>
    {
        public bool IsValid(Candidate entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public IEnumerable<string> BrokenRules(Candidate entity)
        {
            if (String.IsNullOrEmpty(entity.Name))
                yield return "Name must have a value!";

            yield break;
        }
    }
}