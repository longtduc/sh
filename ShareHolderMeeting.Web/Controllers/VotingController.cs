using Application.VotingCards;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class VotingController : Controller
    {
       
        private VotingCardServices _svc;
        // If you are using Dependency Injection, you can delete the following constructor
        public VotingController(VotingCardServices svc)
        {        
            _svc = svc;
        }

        public ViewResult Index()
        {
            return View();
        }      

    }
}

