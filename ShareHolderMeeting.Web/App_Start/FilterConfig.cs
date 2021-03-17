using ExceptionFilterInMVC.Models;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Built-in Filter
            //filters.Add(new HandleErrorAttribute()); 
            //Custom Filter
            filters.Add(new LogCustomExceptionFilter());
        }
    }
}
