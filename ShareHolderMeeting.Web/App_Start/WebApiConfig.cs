using ShareHolderMeeting.Web.App_Start.IoContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShareHolderMeeting.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new NinjectResolver();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
