using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ShareHolders;
using Application.VotingCards;
using Domain.Entities;
using ShareHolderMeeting.Web.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class VotingCardController : Controller
    {
        private readonly IShareHolderContext _context;
        private VotingCardServices _votingCardSvc;

        public VotingCardController(IShareHolderContext context, VotingCardServices vcSvc)
        {
            _context = context;
            _votingCardSvc = vcSvc;
        }

        //Get VotingCards 
        public async Task<JsonResult> GetVotingCards(int votingType)
        {
            VotingCardType type = ToVotingCardType(votingType);

            var allVotingCards = _context.VotingCards.Include("ShareHolder")
                .Where(m => m.VotingCardType == type);
                
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
            }).ToListAsync();

            return Json(await result, JsonRequestBehavior.AllowGet);
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
            if (votingCard == null)
                return Json(null, JsonRequestBehavior.AllowGet);

            var votingCardDto = VotingCardHelper.ToVotingCardDto(votingCard);

            return Json(votingCardDto, JsonRequestBehavior.AllowGet);

        }

        //Update VotingCard 
        public JsonResult Vote(VotingCardDto votingCardDto)
        {

            object result = null;
            var votingCard = _context.VotingCards
                                .Include("VotingCardLines").Where(v=>v.Id ==votingCardDto.Id).FirstOrDefault();
            if (votingCard == null)
                throw new InvalidOperationException();

            //Validate VotingCardVM on server here

            try
            {
                var votingCardLines = VotingCardHelper.ToVotingCardLines(votingCardDto.VotingCardLines);
                votingCard.Vote(votingCardDto.IsInvalid, votingCardLines);
                _context.SaveChanges();

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
            var currentCard = _context.VotingCards.Find(id);            
            if (currentCard != null)
                Json(new { status = false, Message="VotingCard id = {id} not found"}, JsonRequestBehavior.AllowGet);

            currentCard.RevertVoting();

            //_context.InsertOrUpdate(currentCard); //Notes
            _context.SaveChanges();

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }

        //Get VotingResult
        public JsonResult GetVotingResult(int votingType)
        {
            VotingCardType type = ToVotingCardType(votingType);
            VotingResultView result = _votingCardSvc.CreateVotingResultVM(type);             
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
                _votingCardSvc.GenerateVotingCards(new ShareHolderService(_context));
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

