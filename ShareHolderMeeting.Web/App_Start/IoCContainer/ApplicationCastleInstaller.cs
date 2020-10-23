using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using System.Web.Http;
using ShareHolderMeeting.Web.Controllers;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Web.Infrastructure
{
    public class ApplicationCastleInstaller: IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register working dependencies
            

            container.Register(Component.For<IShareHolderRepo>().ImplementedBy<ShareHolderRepo>().LifestylePerWebRequest());
            container.Register(Component.For<IStatementRepo>().ImplementedBy<StatementRepo>().LifestylePerWebRequest());
            container.Register(Component.For<IVotingCardRepo>().ImplementedBy<VotingCardRepo>().LifestylePerWebRequest());
            container.Register(Component.For<VotingCardServices>().ImplementedBy<VotingCardServices>().LifestylePerWebRequest());
            container.Register(Component.For<StatementService>().ImplementedBy<StatementService>().LifestylePerWebRequest());
            container.Register(Component.For<VotingByHandServices>().ImplementedBy<VotingByHandServices>().LifestylePerWebRequest());
            container.Register(Component.For<ShareHolderContext>().ImplementedBy<ShareHolderContext>().LifestylePerWebRequest());
            //Register the MVC controllers one by one 

            //Register all the MVC controllers in the current executing assembly
            var contollers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller)).ToList();
            foreach (var controller in contollers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }

            //// Register all the WebApi controllers within this assembly
            //container.Register(Classes.FromAssembly(typeof(CandidateDSController).Assembly)
            //                          .BasedOn<ApiController>()
            //                          .LifestyleScoped());
        }
    }
}