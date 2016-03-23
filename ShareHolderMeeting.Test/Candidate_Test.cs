using System;
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
            var sut = candidate.IsValidForVotingCard(VotingCardType.BOSVotingCard);
            Assert.IsFalse(sut);
        }
        [TestMethod]
        public void IsIncludedInVotingCard_ShouldReturnTrue()
        {
            var candidate = new Candidate() { Id = 1, Name = "Danh", CandidateType = CandidateType.BODCandidate };
            var sut = candidate.IsValidForVotingCard(VotingCardType.BODVotingCard);
            Assert.IsTrue(sut);
        }
    }
}
