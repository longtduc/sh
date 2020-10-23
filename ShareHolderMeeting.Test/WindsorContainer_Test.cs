using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

using ShareHolderMeeting.Web.Interfaces;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class WindsorContainer_Test
    {
        private WindsorContainer container;

        [TestInitialize]
        public  void Init()
        {
            container = new WindsorContainer();
            container.Register(Component.For<IShareHolderRepo>().ImplementedBy<ShareHolderRepo>());
        }
        [TestMethod]
        public void CheckToSeeIfShareHolderRepoIsRevoked()
        {
            var repo = container.Resolve<IShareHolderRepo>();
            var result = repo.All.ToString();
            Assert.IsNotNull(result);
        }
    }
}
