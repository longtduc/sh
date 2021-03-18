using ExceptionFilterInMVC.Models;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Built-in Filter for global level handling
            //filters.Add(new HandleErrorAttribute());

            //Custom Exception Filter. Should use Application_Error event
            filters.Add(new CustomExceptionFilter());
        }
    }
}
