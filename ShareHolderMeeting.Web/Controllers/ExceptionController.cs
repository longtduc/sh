using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: ForExceptionTest
        public ActionResult WithTryCatch()
        {
            int a = 1;
            int b = 0;
            int c = 0;
            try
            {
                c = a / b; //it would cause exception.  
            }
            catch (Exception ex)
            {
                ViewBag.Message = "WithTryCatch";
                return View("Error");
            }
            finally
            {
            }
            return View();
        }

        [HandleError(ExceptionType = typeof(DivideByZeroException), View = "Error2")]
        [HandleError]
        public ActionResult WithHandleErrorBuiltIn()
        {
            throw new Exception("Unhandled exception with HandleErrorAttribute built-in");
        }

        public ActionResult WithoutHandleErrorBuiltIn()
        {
            throw new NullReferenceException();
        }
       

       
    }
}