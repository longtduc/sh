using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Application.Services;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Services;

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

