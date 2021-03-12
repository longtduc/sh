using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;
using System.Collections.Generic;
using Domain.Entities;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingCard_Test
    {
        private List<Candidate> _candidates;
        private ShareHolder _shareHolder;
        [TestInitialize]
        public void Init()
        {
            _candidates = new List<Candidate>();
            _candidates.Add(new Candidate() { Id = 1, Name = "Danh", CandidateType = CandidateType.BODCandidate });
            _candidates.Add(new Candidate() { Id = 2, Name = "Khoi", CandidateType = CandidateType.BODCandidate });
            _candidates.Add(new Candidate() { Id = 3, Name = "Long", CandidateType = CandidateType.BOSCandidate });
            _candidates.Add(new Candidate() { Id = 4, Name = "Duc", CandidateType = CandidateType.BOSCandidate });

            _shareHolder = new ShareHolder()
            {
                ShareHolderId = 1,
                Name = "Co Dong",
                NumberOfShares = 1000,
                ShareHolderCode = "01",
                StatusAtMeeting = StatusAtMeeting.Attended
            };
        }
        [TestMethod]
        public void CreateVotingCard_ReturnNotNull()
        {
            var sut = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateVotingCard_WhenShareHolderIsABSENT_RaiseException()
        {
            _shareHolder.StatusAtMeeting = StatusAtMeeting.Absent;
            var sut = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            //Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void CreateVotingCard_VotingCardLineIsEqualToTWO()
        {
            var sut = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            Assert.AreEqual(2, sut.VotingCardLines.Count);
        }
    }
}
