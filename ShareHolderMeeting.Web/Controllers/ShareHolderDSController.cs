using Application.Common.Interfaces;
using Application.ShareHolders;
using Domain.Common;
using Domain.Entities;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class ShareHolderDSController : ApiController
    {
        private IShareHolderContext _context;

        private ShareHodlerValidator _shareHolderValidator;

        public ShareHolderDSController(IShareHolderContext repo, ShareHodlerValidator val)
        {
            _context = repo;
            _shareHolderValidator = val;
        }
        public IQueryable<ShareHolderDto> Get()
        {
            return _context.ShareHolders.Select(sh => new ShareHolderDto()
            {
                ShareHolderId = sh.ShareHolderId,
                Name = sh.Name,
                NumberOfShares = sh.NumberOfShares,
                ShareHolderCode = sh.ShareHolderCode,
                StatusAtMeeting = sh.StatusAtMeeting
            }) ;
        }

        public IEnumerable<ShareHolder> Get(int pageIndex, int pageSize)
        {
            return _context.ShareHolders.AsEnumerable().Skip(pageIndex * pageSize).Take(pageSize);
        }

        public ShareHolder Get(int id)
        {

            var shareHolder = _context.ShareHolders.Find(id);
            if (shareHolder == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return shareHolder;
        }

        public HttpResponseMessage Post([FromBody]ShareHolder shareHolder)
        {
            //Validate and returnn errors if any
            var response = new HttpResponseMessage();
            var brokerRules = _shareHolderValidator.BrokenRules(shareHolder);
            if (brokerRules.Count() > 0)
            {
                var message = CoreHelper.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }

            //Persist
            try
            {
                _context.ShareHolders.Add(shareHolder);
                _context.SaveChanges();
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

            var shareHolder = _context.ShareHolders.Find(id);
            if (shareHolder == null)
                return null;
            _context.ShareHolders.Remove(shareHolder);
            _context.SaveChanges();
            return shareHolder;
        }

        public HttpResponseMessage Put([FromBody]ShareHolder shareHolder)
        {
            //Validate and Return Errors
            var response = new HttpResponseMessage();
            var brokerRules = _shareHolderValidator.BrokenRules(shareHolder);
            if (brokerRules.Count() > 0)
            {
                var message = CoreHelper.MergeErrors(brokerRules);
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                return response;
            }
            var entity = _context.ShareHolders.Find(shareHolder.ShareHolderId);
            if (entity ==  null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"ShareHolder {shareHolder.ShareHolderId} not found");
                return response;
            }
            //Persist
            try
            {
                entity.Name = shareHolder.Name;
                entity.NumberOfShares = shareHolder.NumberOfShares;
                entity.ShareHolderCode = shareHolder.ShareHolderCode;

                _context.SaveChanges(); 
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