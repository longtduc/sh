using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace ShareHolderMeeting.Web.Infrastructure
{
    public class RangeExceptionAttribute : FilterAttribute
    {

        //public void OnException(ExceptionContext filterContext)
        //{
        //    if (!filterContext.ExceptionHandled &&
        //        filterContext.Exception is ArgumentOutOfRangeException)
        //    {
        //        filterContext.Result
        //            = new RedirectResult("~/Content/RangeErrorPage.html");
        //        filterContext.ExceptionHandled = true;
        //    }
        //}
      
    }
}