using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using ShareHolderMeeting.Web.App_Start;
using ShareHolderMeeting.Web.Infrastructure;
using System.Diagnostics;
using Application.Common.Exceptions;
using Persistence;

namespace ShareHolderMeeting.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private IWindsorContainer container;
        protected void Application_Start()
        {

            //AreaRegistration.RegisterAllAreas(); //Used for Areas
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Debug.WriteLine("Write from Debug.Write()");
            

            // Initialize Castle & install application components
            var container = new WindsorContainer();
            container.Install(new ApplicationCastleInstaller());

            // Create the Controller Factory
            var castleControllerFactory = new CastleControllerFactory(container);

            // Add the Controller Factory into the MVC web request pipeline
            ControllerBuilder.Current.SetControllerFactory(castleControllerFactory);

            ////Config for Web API
            //// Configure WebApi to use a new CastleDependencyResolver as its dependency resolver
            //GlobalConfiguration.Configuration.DependencyResolver = new CastleDependencyResolver(container);

            //// Configure WebApi to use the newly configured GlobalConfiguration complete with Castle dependency resolver
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //get the last error of server  
            var error = Server.GetLastError();

            //get the http code of error: 400, 404, 500...  
            var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

            //write log  

            new ExceptionsLog(new ShareHolderContext()).SaveToLogTable(error.Message);

            //send email  

            //clear response stream  
            Response.Clear();

            //clear server's error  
            Server.ClearError();

            //redirect to error page  
            Response.Redirect(string.Format("~/Error/Index/{0}", code));
        }
    }
}

