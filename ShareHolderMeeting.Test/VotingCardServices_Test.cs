using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Services;
//using ShareHolderMeeting.Web.Services;
//using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingCardServices_Test
    {
        private VotingCardServices _svc;
      
        [TestInitialize]
        public void Init() 
        {
            _svc = new ShareHolderMeeting.Web.Services.VotingCardServices();         
        }


        [TestMethod]
        [Ignore]
        public void GenerateVotingCardsShouldRecreateVotingCards()
        {
            _svc.GenerateVotingCards();
        }
    
        
    }
}
