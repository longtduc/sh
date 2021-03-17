using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error  
        public ActionResult Index(int id)
        {
            Response.StatusCode = id;
            ViewBag.StatusCode = id;
            return View();
        }
    }
}