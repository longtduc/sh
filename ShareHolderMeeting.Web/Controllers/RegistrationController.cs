using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Services;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
       
        private VotingCardServices _svc;
        // If you are using Dependency Injection, you can delete the following constructor
        public RegistrationController()
        {        
            _svc = new VotingCardServices();
        }

        public ViewResult Index()
        {
            return View();
        }
        public JsonResult UpdateStatus(int shareHolderId, int newStatus)
        {
            object result = null;
            try
            {              
                _svc.ChangeShareHolderStatus(shareHolderId, newStatus);
                result = new { status = true };
            }
            catch (Exception ex)
            {
                result = new { status = false, message = ex.Message };

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}

