using Application.ShareHolders;
using System;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private ShareHolderService _svc;
        // If you are using Dependency Injection, you can delete the following constructor
        public RegistrationController(ShareHolderService service)
        {
            _svc = service;
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

