using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;
using System.Collections.Generic;
using ShareHolderMeeting.Web.ViewModel;
using System.Linq;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingByHandResultVM_Test
    {
        private List<VotingByHand> _votingByHands;
        [TestInitialize]
        public void Init()
        {
            _votingByHands = new List<VotingByHand>();

            //Adding the FIRST votingByHand
            var shareHolder = new ShareHolder()
            {
                ShareHolderId = 1,
                NumberOfShares = 1000,
                Name = "Long",
                ShareHolderCode = "01"
            };
            var voting = CreateVotingByHand(shareHolder, VotingOption.Yes);
            _votingByHands.Add(voting);

            //Adding the SECOND votingByHand
            var shareHolder2 = new ShareHolder()
            {
                ShareHolderId = 2,
                NumberOfShares = 2000,
                Name = "Diem",
                ShareHolderCode = "02"
            };

            var voting2 = CreateVotingByHand(shareHolder2, VotingOption.Yes);
            _votingByHands.Add(voting2);

        }

        [TestMethod]
        public void Create_StatementResultsCountShouldEqualToTwo()
        {
            var sut = new VotingByHandResultVM(_votingByHands);
            Assert.AreEqual(2, sut.StatementResults.Count);
        }

        [TestMethod]
        public void Create_TotalNumberOfSharesShouldEqualTo3000()
        {
            var sut = new VotingByHandResultVM(_votingByHands);
            Assert.AreEqual(3000, sut.TotalNumberOfShares);
        }

        [TestMethod]
        public void Create_Accumulate_AmountOfNOAndYESIsEqualToZERO()
        {
            var sut = new VotingByHandResultVM(_votingByHands);
          
            Assert.AreEqual(6000, sut.StatementResults.Sum(s => s.AmtOfSharesYes));
            Assert.AreEqual(0, sut.StatementResults.Sum(s => s.AmtOfSharesNo));
            Assert.AreEqual(0, sut.StatementResults.Sum(s => s.AmtOfSharesOther));
        }

        [TestMethod]
        public void Create_Accumulate_AmountOfYESandNOIsNotEqualToZero()
        {
            foreach (var line in _votingByHands.ElementAt(0).VotingByHandLines)
            {
                line.VotingOption = VotingOption.No;
            }
            var sut = new VotingByHandResultVM(_votingByHands);
            //Assert.AreEqual(3000, sut.StatementResults.Sum(s=>s.AmtOfSharesYes));
            Assert.AreEqual(4000, sut.StatementResults.Sum(s => s.AmtOfSharesYes));
            Assert.AreEqual(2000, sut.StatementResults.Sum(s => s.AmtOfSharesNo));
            Assert.AreEqual(0, sut.StatementResults.Sum(s => s.AmtOfSharesOther));
        }

        [TestMethod]
        public void Create_Accumulate_YesRateShouldEqualTo100()
        {
            var sut = new VotingByHandResultVM(_votingByHands);
            //Assert.AreEqual(3000, sut.StatementResults.Sum(s=>s.AmtOfSharesYes));
            Assert.AreEqual(100, sut.StatementResults.ElementAt(0).YesRate);
            Assert.AreEqual(0, sut.StatementResults.ElementAt(0).NoRate);
            Assert.AreEqual(0, sut.StatementResults.ElementAt(0).OtherRate);
        }


        private VotingByHand CreateVotingByHand(ShareHolder shareHolder, VotingOption option)
        {
            var shId = shareHolder.ShareHolderId;
            var voting = new VotingByHand()
            {
                Id = shareHolder.ShareHolderId,
                NumberOfShares = shareHolder.NumberOfShares,
                ShareHolderCode = shareHolder.ShareHolderCode,
                ShareHolderId = shareHolder.ShareHolderId,
                ShareHolderName = shareHolder.Name
            };

            voting.VotingByHandLines.Add(
                new VotingByHandLine()
                {
                    Id = shId * 100,
                    StatementId = 1,
                    StatementDesc = "BC HDQT",
                    VotingOption = option
                });


            voting.VotingByHandLines.Add(
            new VotingByHandLine()
            {
                Id = shId * 100 + 1,
                StatementId = 2,
                StatementDesc = "BC BTGD",
                VotingOption = option
            });

            return voting;

        }
    }
}
