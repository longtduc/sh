//using ShareHolderMeeting.Web.Cqs;
using Application.Common.Interfaces;
using Domain.Entities;
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
        private readonly IShareHolderContext _context;
        
        public CandidateQueryHandler(IShareHolderContext context)
        {
            _context = context;
        }
        public IEnumerable<Candidate> Handler(GetCandidatesQuery _query)
        {
            return _context.Candidates.Where(c=>c.CandidateType == _query._candidateType).ToList();
        }
    }
}