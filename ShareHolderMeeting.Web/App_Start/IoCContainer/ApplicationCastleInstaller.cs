using Application.Common.Interfaces;
using Application.ShareHolders;
using Application.Statements;
using Application.VotingByHands;
using Application.VotingCards;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Persistence;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

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


            container.Register(Component.For<ShareHolderService>().ImplementedBy<ShareHolderService>().LifestylePerWebRequest());
            container.Register(Component.For<VotingCardServices>().ImplementedBy<VotingCardServices>().LifestylePerWebRequest());
            container.Register(Component.For<StatementService>().ImplementedBy<StatementService>().LifestylePerWebRequest());
            container.Register(Component.For<VotingByHandServices>().ImplementedBy<VotingByHandServices>().LifestylePerWebRequest());
            container.Register(Component.For<IShareHolderContext>().ImplementedBy<ShareHolderContext>().LifestylePerWebRequest());
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