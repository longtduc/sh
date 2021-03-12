using Application.Candidates;
using Domain.Entities;
using ShareHolderMeeting.Web.Queries;
using ShareHolderMeeting.Web.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize(Roles = "Administrators")]
    
    public class CandidateDSController : ApiController
    {
        private readonly CandidateQueryHandler _queryHandler;
        private readonly CandidateCommandHandler _commandHander;

        public CandidateDSController(CandidateQueryHandler queryHandler, CandidateCommandHandler commandHander)
        {
            _queryHandler = queryHandler;
            _commandHander = commandHander;
        }

        public IEnumerable<Candidate> Get(CandidateType type)
        {

            var query = new GetCandidatesQuery(type);
            return _queryHandler.Handler(query);
        }

        public HttpResponseMessage Post([FromBody] Candidate candidate)
        {
            var response = new HttpResponseMessage();
            var command = new CreateCandidateCommand(candidate);
            var result = _commandHander.Handler(command);
            if (result.IsSuccess)
            {
                response = Request.CreateResponse<Candidate>(HttpStatusCode.Created, result.Value);
                response.Headers.Location = new Uri(Request.RequestUri, "/Api/BODCandidateDS/" + result.Value);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, result.Error);
            }
            return response;
        }

        public Candidate Delete(int id)
        {
            var cmd = new RemoveCandidateCommand(id);
            return (_commandHander.Handler(cmd).Value);
        }

        public HttpResponseMessage Put([FromBody] Candidate candidate)
        {
            var response = new HttpResponseMessage();
            var cmd = new UpdateCandidateCommand(candidate);
            var result = _commandHander.Handler(cmd);

            if (result.IsSuccess)
            {
                response = Request.CreateResponse<Candidate>(HttpStatusCode.OK, candidate);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, result.Error);
            }
            return response;
        }

    }
}