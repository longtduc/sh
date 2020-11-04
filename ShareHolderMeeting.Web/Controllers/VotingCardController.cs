using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Services;
using ShareHolderMeeting.Web.ViewModel;
using ShareHolderMeeting.Web.Interfaces;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class VotingCardController : Controller
    {
        private readonly IVotingCardRepo _votingCardRepo;
        private VotingCardServices _votingCardSvc;

        public VotingCardController(IVotingCardRepo vcRepo, VotingCardServices vcSvc)
        {
            _votingCardRepo = vcRepo;
            _votingCardSvc = vcSvc;
        }

        //Get VotingCards 
        public JsonResult GetVotingCards(int votingType)
        {
            VotingCardType type = ToVotingCardType(votingType);

            var allVotingCards = _votingCardRepo
                .AllIncluding(m => m.ShareHolder)
                .Where(m => m.VotingCardType == type && m.ShareHolderId != null )
                .ToList();
            var result = allVotingCards.Select(m => new
            {
                m.Id,
                m.IsInvalid,
                m.IsVoted,
                m.NumberOfCandidates,
                NumberOfShares = m.ShareHolder.NumberOfShares,
                ShareHolderCode = m.ShareHolder.ShareHolderCode,
                ShareHolderName = m.ShareHolder.Name,
                AmtAlreadyVoted = m.AmtAlreadyVoted,
                ShareHolderId = m.ShareHolder.ShareHolderId
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private VotingCardType ToVotingCardType(int votingType)
        {
            return (VotingCardType)votingType;

        }

        //Get VotingCard to input VotingCard (from shareholders' Voting Card)
        public JsonResult GetVotingCard(int id)
        {
            var votingCard = _votingCardSvc.GetVotingCard(id);
            //For json serialization
            foreach (var line in votingCard.VotingCardLines)
            {
                line.VotingCard = null;
            }
            return Json(votingCard, JsonRequestBehavior.AllowGet);

        }

        //Update VotingCard 
        public JsonResult UpdateVotingCard(VotingCard votingCard)
        {

            object result = null;            
            try
            {
                votingCard.IsVoted = true;
                votingCard.SetAmtAlreadyVoted();

                _votingCardRepo.UpdateGraph(votingCard);

                //Commit
                _votingCardRepo.Save();
                var returnedObj = new { AmtAlreadyVoted = votingCard.AmtAlreadyVoted };
                result = new { Status = true, Message = "", ReturnedObj = returnedObj };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Reverting VotingCard
        public JsonResult RevertVotingCard(int id)
        {
            var currentCard = _votingCardRepo.Find(id);

            currentCard.IsInvalid = false;
            currentCard.IsVoted = false;
            currentCard.SetAmtAlreadyVoted();

            _votingCardRepo.InsertOrUpdate(currentCard);
            _votingCardRepo.Save();

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        //Get VotingResult
        public JsonResult GetVotingResult(int votingType)
        {
            VotingCardType type = ToVotingCardType(votingType);
            var result = _votingCardSvc.CreateVotingResultVM(type);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Used for generating automatically All Voting Cards (Due to changing the BOD/BOS candidate after registering shareholder)
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GenerateVotingCards()
        {
            object result = null;
            try
            {
                var _svc = new VotingCardServices();
                _svc.GenerateVotingCards();
                result = new { Status = true, Message = "You have just generated All Voting Cards" };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {

        }
    }
}

