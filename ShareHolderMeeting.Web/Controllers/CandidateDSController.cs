using Domain.Entities;
using ShareHolderMeeting.Web.Commands;
using ShareHolderMeeting.Web.Commands.CqsForCandidate;
using ShareHolderMeeting.Web.CqsForCandidate;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Queries;
using ShareHolderMeeting.Web.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShareHolderMeeting.Web.Controllers
{
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
            var commandResult = _commandHander.Handler(command);
            if (commandResult.Success)
            {
                response = Request.CreateResponse<Candidate>(HttpStatusCode.Created, commandResult.ReturnObj as Candidate);
                response.Headers.Location = new Uri(Request.RequestUri, "/Api/BODCandidateDS/" + (commandResult.ReturnObj as Candidate).Id.ToString());
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, commandResult.Message);
            }
            return response;
        }

        public Candidate Delete(int id)
        {
            var removeCmd = new RemoveCandidateCommand(id);
            return (_commandHander.Handler(removeCmd).ReturnObj as Candidate);
        }

        public HttpResponseMessage Put([FromBody] Candidate candidate)
        {
            var response = new HttpResponseMessage();
            var cmd = new UpdateCandidateCommand(candidate);
            var cmdResult = _commandHander.Handler(cmd);

            if (cmdResult.Success)
            {
                response = Request.CreateResponse<Candidate>(HttpStatusCode.OK, candidate);
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, cmdResult.Message);
            }
            return response;
        }

    }
}