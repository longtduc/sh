using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Interfaces;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ShareHolderController : Controller
    {
        private readonly IShareHolderRepo shareholderRepo;

        public ShareHolderController(IShareHolderRepo shareholderRepository)
        {
            this.shareholderRepo = shareholderRepository;
        }

        public ViewResult Index()
        {
            return View(shareholderRepo.All);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //shareholderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

