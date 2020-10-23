//using ShareHolderMeeting.Web.Cqs.Data;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Queries
{
    public class GetCandidatesQuery 
    {
        public GetCandidatesQuery(CandidateType candidateType)
        {
            _candidateType = candidateType;
        }

        public CandidateType _candidateType { get; }
    }
   
}