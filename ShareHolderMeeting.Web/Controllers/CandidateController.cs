using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class CandidateController : Controller
    {
        public ViewResult Index(int type)
        {
            ViewBag.Type = type;
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

