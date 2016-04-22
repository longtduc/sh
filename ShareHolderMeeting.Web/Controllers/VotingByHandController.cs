using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Services;
using ShareHolderMeeting.Web.ViewModel;
using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models.CoreServices;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class VotingByHandController : Controller
    {
        private readonly IShareHolderRepo _shareHolderRepo;
      
        private VotingByHandServices _votingByHandSvc;

        // If you are using Dependency Injection, you can delete the following constructor
        public VotingByHandController()
            : this(new ShareHolderRepository(), new VotingByHandServices())
        {
        }

        public VotingByHandController(IShareHolderRepo shRepo, VotingByHandServices vcs)
        {
            _shareHolderRepo = shRepo;
            _votingByHandSvc = vcs;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetVotingByHands()
        {

            var result = _votingByHandSvc.GetVotingByHands();
            //for serializing 
            foreach (var voting in result)
            {
                foreach (var line in voting.VotingByHandLines)
                {
                    line.VotingByHand = null;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVotingByHand(int id)
        {
            var result = _votingByHandSvc.GetVotingByHand(id);
            foreach (var line in result.VotingByHandLines)
            {
                line.VotingByHand = null;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GenerateVotingByHands()
        {
            dynamic result = new { Status = true };
            try
            {
                _votingByHandSvc.GenerateVotingByHands();
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateVoingByHand(VotingByHand entity)
        {
            dynamic result = new { Status = true };
            try
            {
                _votingByHandSvc.UpdateVoingByHand(entity);
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetVotingByHandResult()
        {
            var result = _votingByHandSvc.CreateVotingByHandResult();            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}

