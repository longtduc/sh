using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;
using System.Collections.Generic;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingByHand_Test
    {
        private ShareHolder _shareHolder;
        private List<Statement> _statements;
        [TestInitialize]
        public void Init() 
        {
            _shareHolder = new ShareHolder() { ShareHolderId = 1, Name = "Long", NumberOfShares = 1000, ShareHolderCode = "01", StatusAtMeeting = StatusAtMeeting.Attended };
            _statements = new List<Statement>();
            _statements.Add(new Statement() { Id = 1, Description = "BC KQKD" });
            _statements.Add(new Statement() { Id = 2, Description = "BC BKS" });
        }
        [TestMethod]
        public void CreateVotingByHand_ShouldBeReturnTWO()
        {
            var votingByHand = new VotingByHand(_shareHolder, _statements);
                 
            Assert.AreEqual(2, votingByHand.VotingByHandLines.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVotingByHand_WhenShareHolderAbsent_ReturnException()
        {
            _shareHolder.StatusAtMeeting = StatusAtMeeting.Absent;
            var votingByHand = new VotingByHand(_shareHolder, _statements);
           
        }
    }
}
