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
		private readonly IShareHolderRepo shareholderRepository;

		// If you are using Dependency Injection, you can delete the following constructor
		public ShareHolderController() : this(new ShareHolderRepository())
		{
		}

		public ShareHolderController(IShareHolderRepo shareholderRepository)
		{
			this.shareholderRepository = shareholderRepository;
		}
	  
		public ViewResult Index()
		{
			return View(shareholderRepository.All);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				shareholderRepository.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

