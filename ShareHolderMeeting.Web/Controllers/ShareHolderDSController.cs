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
    //[Authorize]
    public class ShareHolderDSController : ApiController
    {
        private ShareHolderRepository _shareHolderRepo;

        private ShareHodlerValidator _shareHolderValidator;

        public ShareHolderDSController()
        {
            _shareHolderRepo = new ShareHolderRepository();
            _shareHolderValidator = new ShareHodlerValidator();

        }


        public IQueryable<ShareHolder> Get()
        {
            return _shareHolderRepo.All;
        }

        public IEnumerable<ShareHolder> Get(int pageIndex, int pageSize)
        {
            return _shareHolderRepo.All.AsEnumerable().Skip(pageIndex * pageSize).Take(pageSize);
        }

        public ShareHolder Get(int id)
        {

            var shareHolder = _shareHolderRepo.All.First(m => m.ShareHolderId == id);
            if (shareHolder == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return shareHolder;
        }

        public HttpResponseMessage Put([FromBody]ShareHolder shareHolder)
        {
            //Validate and returnn errors if any
            var response = new HttpResponseMessage();
            if (!_shareHolderValidator.IsValid(shareHolder))
            {
                var brokerRules = _shareHolderValidator.BrokenRules(shareHolder);
                var message = InputErrors.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }

            //Persist
            try
            {
                _shareHolderRepo.InsertOrUpdate(shareHolder);
                _shareHolderRepo.Save();
                response = Request.CreateResponse<ShareHolder>(HttpStatusCode.Created, shareHolder);
                response.Headers.Location = new Uri(Request.RequestUri, "/Api/ShareHolderDS/" + shareHolder.ShareHolderId.ToString());
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message, ex);

            }
            return response;
        }

        public ShareHolder Delete(int id)
        {

            var shareHolder = _shareHolderRepo.Find(id);
            _shareHolderRepo.Delete(id);
            _shareHolderRepo.Save();
            return shareHolder;
        }

        public HttpResponseMessage Post([FromBody]ShareHolder shareHolder)
        {
            //Validate and Return Errors
            var response = new HttpResponseMessage();
            if (!_shareHolderValidator.IsValid(shareHolder))
            {
                var brokerRules = _shareHolderValidator.BrokenRules(shareHolder);
                var message = InputErrors.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }

            //Persist
            try
            {

                _shareHolderRepo.InsertOrUpdate(shareHolder);
                _shareHolderRepo.Save();
                response = Request.CreateResponse<ShareHolder>(HttpStatusCode.OK, shareHolder);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;

        }


    }
}