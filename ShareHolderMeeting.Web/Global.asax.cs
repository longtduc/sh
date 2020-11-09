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

namespace ShareHolderMeeting.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private IWindsorContainer container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

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
    }
}

