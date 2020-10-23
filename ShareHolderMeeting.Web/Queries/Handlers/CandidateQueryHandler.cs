//using ShareHolderMeeting.Web.Cqs;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Queries.Handlers
{
    public class CandidateQueryHandler
    {
        private readonly ShareHolderContext _context;
        
        public CandidateQueryHandler(ShareHolderContext context)
        {
            _context = context;
        }
        public IEnumerable<Candidate> Handler(GetCandidatesQuery _query)
        {
            return _context.Candidates.Where(c=>c.CandidateType == _query._candidateType).ToList();
        }
    }
}