using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    public class OnExceptionController : Controller
    {
        // GET: OnException
        public ActionResult ThrowException()
        {
            throw new DivideByZeroException();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            string action = filterContext.RouteData.Values["action"].ToString();
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error2",
            };
            //}
        }
    }
}