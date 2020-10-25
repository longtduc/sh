using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Interfaces;
using System.Linq;
using ShareHolderMeeting.Web.Models.CoreServices;
using System.Transactions;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Services;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingByHandServices_Test
    {
        private IShareHolderRepo _shareHolderRepo;
        private VotingByHandRepo _votingByHandRepo;
        private StatementRepo _statementRepo;
        private VotingByHandServices _svc;
        [TestInitialize]
        public void Init()
        {
            _shareHolderRepo = new ShareHolderRepo(new ShareHolderContext());
            _votingByHandRepo = new VotingByHandRepo(new ShareHolderContext());
            _statementRepo = new StatementRepo(new ShareHolderContext());
            _svc = new VotingByHandServices();

        }
        [TestMethod]
        public void CreateAVotingByHand_ShouldCreateOneMoreVotingByHand()
        {
            using (var scope = new TransactionScope())
            {

                //Arrange 
                var statementCount = _statementRepo.All.Count();
                var votingByHandCount = _votingByHandRepo.All.Count();

                var shareHolder = _shareHolderRepo.All.FirstOrDefault();
                shareHolder.StatusAtMeeting = StatusAtMeeting.Attended;

                var svc = new VotingByHandServices();
                //Action

                _svc.CreateVotingByHand(shareHolder);
                _svc.Save();
                //Assert
                var votingByHandCountAfter = _votingByHandRepo.All.Count();
                Assert.AreEqual(votingByHandCount + 1, votingByHandCountAfter);

            }
        }

        [TestMethod]
        public void RemoveVotingCardByHands_ShouldRemoveALL()
        {
            using (var scope = new TransactionScope())
            {
                //Action
                _svc.RemoveVotingByHands();
                _svc.Save();
                //Assert
                var votingByHandCountAfter = _votingByHandRepo.All.Count();

                Assert.AreEqual(0, votingByHandCountAfter);
            }
        }

        [TestMethod]
        public void GenerateVotingByHands_ShoulReturnNotNull() 
        {
            var shareHolderCount = _shareHolderRepo.All
                .Where(s=>s.StatusAtMeeting != StatusAtMeeting.Absent)
                .Count();
            _svc.GenerateVotingByHands();
            var votingByHandCount = _votingByHandRepo.All.Count();

            Assert.AreEqual(shareHolderCount, votingByHandCount);        
        }
        [TestMethod]
        public void GetVotingByHand_ShouldReturnNotNull() 
        {
            var votingByHand = _votingByHandRepo.All.FirstOrDefault();
            if (votingByHand == null)
                return;
            var sut = _svc.GetVotingByHand(votingByHand.Id);

            Assert.IsNotNull(sut);
            
        }
        [TestMethod]
        public void UpdateVoingByHand_ShouldUpdateVotingByHandLine() 
        {
            var votingByHand = _votingByHandRepo.
                AllIncluding(v=>v.VotingByHandLines).
                FirstOrDefault();
            if (votingByHand == null)
                return;
            
            foreach (var line in votingByHand.VotingByHandLines)
            {
                line.VotingOption = VotingOption.Other;
            }
            _svc.UpdateVoingByHand(votingByHand);
            votingByHand = _votingByHandRepo.
                AllIncluding(v => v.VotingByHandLines).
                FirstOrDefault();
            var first = votingByHand.VotingByHandLines.ElementAt(0);
            Assert.AreEqual(VotingOption.Other, first.VotingOption);
            
            

        
        }
    }
}
