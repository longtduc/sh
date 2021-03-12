using System;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class Candidate_Test
    {
        [TestMethod]
        public void IsIncludedInVotingCard_ShouldReturnFalse()
        {
            var candidate = new Candidate() { Id = 1, Name = "Danh", CandidateType = CandidateType.BODCandidate };
            var sut = candidate.CanBeVoted(VotingCardType.BOSVotingCard);
            Assert.IsFalse(sut);
        }
        [TestMethod]
        public void IsIncludedInVotingCard_ShouldReturnTrue()
        {
            var candidate = new Candidate() { Id = 1, Name = "Danh", CandidateType = CandidateType.BODCandidate };
            var sut = candidate.CanBeVoted(VotingCardType.BODVotingCard);
            Assert.IsTrue(sut);
        }


    }


}
