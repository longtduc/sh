using Application.Common.Exceptions;
using Application.Statements;
using Domain.Entities;
using Persistence;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExceptionFilterInMVC.Models
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action:" + actionName +
                                 "Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace;

                //1-saving the data in a text file called Log.txt                
                //File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), Message);

                //2 -save this in a database
                //new ExceptionsLog(new ShareHolderContext()).SaveToLogTable(message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };

            }
        }
       
    }
}