using Application.Common.Interfaces;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Injection;
using Persistence;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Queries.Handlers;
//using ShareHolderMeeting.Web.Requests.Queries.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace ShareHolderMeeting.Web.App_Start.IoContainer
{
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            //kernel.Bind<ICandidateRepo>().To<CandidateRepo>().InSingletonScope(); //Notes
            kernel.Bind<IShareHolderContext>().To<ShareHolderContext>().InTransientScope();
            kernel.Bind<CandidateValidator>().To<CandidateValidator>().InTransientScope();
            kernel.Bind<ShareHodlerValidator>().To<ShareHodlerValidator>().InTransientScope();
            kernel.Bind<ShareHolderContext>().To<ShareHolderContext>().InSingletonScope();
            kernel.Bind<CandidateQueryHandler>().To<CandidateQueryHandler>().InTransientScope();

            return kernel;
        }
    }
}