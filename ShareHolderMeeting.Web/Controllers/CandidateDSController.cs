using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Models.CoreServices;
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
        private CandidateRepo _candidateRepo;
        private CandidateValidator _candidateValidator;
        public CandidateDSController()
        {
            _candidateRepo = new CandidateRepo();
            _candidateValidator = new CandidateValidator();
        }

        public IQueryable<Candidate> Get(int type)
        {
            var enumType = ToCandidateType(type);
            return _candidateRepo.All
                    .Where(c => c.CandidateType == enumType);
        }

        private CandidateType ToCandidateType(int type)
        {

            return (CandidateType)type;
        }

        public HttpResponseMessage Put([FromBody] Candidate candidate)
        {
            var response = new HttpResponseMessage();

            //Validate and return error if any
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);
                var message = InputErrors.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }


            //Persist
            try
            {
                _candidateRepo.InsertOrUpdate(candidate);
                _candidateRepo.Save();
                response = Request.CreateResponse<Candidate>(HttpStatusCode.Created, candidate);
                response.Headers.Location = new Uri(Request.RequestUri, "/Api/BODCandidateDS/" + candidate.Id.ToString());
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        public Candidate Delete(int id)
        {
            var candidateToDelete = _candidateRepo.Find(id);
            _candidateRepo.Delete(id);
            _candidateRepo.Save();
            return candidateToDelete;
        }

        public HttpResponseMessage Post([FromBody]Candidate candidate)
        {
            var response = new HttpResponseMessage();          

            //Validate and return error if any
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);
                var message = InputErrors.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }

            //Persist
            try
            {               
                _candidateRepo.InsertOrUpdate(candidate);
                _candidateRepo.Save();
                response = Request.CreateResponse<Candidate>(HttpStatusCode.OK, candidate);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message, ex);
            }
            return response;
        }

    }
}