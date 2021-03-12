using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShareHolderMeeting.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities;
using Application.VotingCards;

namespace ShareHolderMeeting.Test
{
    [TestClass]
    public class VotingResultView_Test
    {
        private ShareHolder _shareHolder;
        private List<Candidate> _candidates;
        private ShareHolder _shareHolder2;
        [TestInitialize]
        public void Init()
        {
            //Create two ShareHolder
            _shareHolder = new ShareHolder()
            {
                ShareHolderId = 1,
                Name = "Long",
                ShareHolderCode = "01",
                NumberOfShares = 1000,
                StatusAtMeeting = StatusAtMeeting.Attended
            };
            _shareHolder2 = new ShareHolder()
            {
                ShareHolderId = 2,
                Name = "Diem",
                ShareHolderCode = "02",
                NumberOfShares = 1000,
                StatusAtMeeting = StatusAtMeeting.Attended
            };
            //Create a list of Candidates
            _candidates = new List<Candidate>();
            _candidates.Add(new Candidate() { Id = 1, Name = "A", CandidateType = CandidateType.BODCandidate });
            _candidates.Add(new Candidate() { Id = 2, Name = "B", CandidateType = CandidateType.BODCandidate });
            _candidates.Add(new Candidate() { Id = 3, Name = "C", CandidateType = CandidateType.BOSCandidate });
        }

        [TestMethod]
        public void CallConstructorWithCandidateTypeOfBOD_ReturnTwo()
        {
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            Assert.IsTrue(sut.CandidateLines.Count == 2);
        }

        [TestMethod]
        public void CallConstructorWithCandidateTypeOfBOS_ReturnOne()
        {
            var sut = new VotingResultView(_candidates, VotingCardType.BOSVotingCard);
            Assert.IsTrue(sut.CandidateLines.Count == 1);
        }
        [TestMethod]
        public void AccumulateForRoot_IfAccumulateIsPRIVATE_METHOD()
        {
            //Create a BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;

            //Create BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);

            //Set argument for constructor
            var types = new Type[] { typeof(List<Candidate>), typeof(VotingCardType) };
            var values = new Object[] { _candidates, VotingCardType.BODVotingCard };
            //Create instance using arguments above
            PrivateObject sut = new PrivateObject(typeof(VotingResultView), types, values);
            sut.Invoke("AccumulateForRoot", votingCards);

            //Assert properties       

            Assert.AreEqual(1, sut.GetProperty("NumberOfVotingCardsIssured"));
            Assert.AreEqual(1, sut.GetProperty("NumberOfVotingCardsReceived"));
            Assert.AreEqual(1, sut.GetProperty("NumberOfVotingCardsValidated"));
            Assert.AreEqual(0, sut.GetProperty("NumberOfVotingCardNotReceived"));

            Assert.AreEqual(2000, sut.GetProperty("VotingCardAmtIssured"));
            Assert.AreEqual(2000, sut.GetProperty("VotingCardAmtReceived"));
            Assert.AreEqual(0, sut.GetProperty("VotingCardAmtNotReceived"));

            Assert.AreEqual(100M, sut.GetProperty("VotingCardAmtReceivedRate"));
            Assert.AreEqual(0M, sut.GetProperty("VotingCardAmtNotReceivedRate"));

        }

        [TestMethod]
        public void AccumulateForRoot_OneVotingCardAndReceiveOneValidVotingCard()
        {
            //Create a BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;

            //Create BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);



            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForRoot(votingCards);

            Assert.AreEqual(1, sut.NumberOfVotingCardsIssured);
            Assert.AreEqual(1, sut.NumberOfVotingCardsReceived);
            Assert.AreEqual(1, sut.NumberOfVotingCardsValidated);
            Assert.AreEqual(0, sut.NumberOfVotingCardNotReceived);

            Assert.AreEqual(2000, sut.VotingCardAmtIssured);
            Assert.AreEqual(2000, sut.VotingCardAmtReceived);
            Assert.AreEqual(0, sut.VotingCardAmtNotReceived);

            Assert.AreEqual(100M, sut.VotingCardAmtReceivedRate);
            Assert.AreEqual(0M, sut.VotingCardAmtNotReceivedRate);

        }

        [TestMethod]
        public void AccumulateForRoot_TwoVotingCardAndReceiveTwoAndOneIsInvalid()
        {
            //Create two Voted BODVotingCards, the second is INVALID            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;

            var card2 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card2.IsVoted = true;
            card2.IsInvalid = true;

            //BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);
            votingCards.Add(card2);

            //Act
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForRoot(votingCards);
            //Assert
            Assert.AreEqual(2, sut.NumberOfVotingCardsIssured);
            Assert.AreEqual(2, sut.NumberOfVotingCardsReceived);
            Assert.AreEqual(1, sut.NumberOfVotingCardsValidated);
            Assert.AreEqual(0, sut.NumberOfVotingCardNotReceived);

            Assert.AreEqual(4000, sut.VotingCardAmtIssured);
            Assert.AreEqual(4000, sut.VotingCardAmtReceived);
            Assert.AreEqual(0, sut.VotingCardAmtNotReceived);

            Assert.AreEqual(100, sut.VotingCardAmtReceivedRate);
            Assert.AreEqual(0, sut.VotingCardAmtNotReceivedRate);
        }

        [TestMethod]
        public void AccumulateForRoot_TwoVotingCardAndReceiveOneValidVotingCard()
        {
            //Create two Voted BODVotingCards, the second is NOT VOTED            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;
            var card2 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card2.IsVoted = false;

            //BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);
            votingCards.Add(card2);

            //Act
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForRoot(votingCards);
            //
            Assert.AreEqual(2, sut.NumberOfVotingCardsIssured);
            Assert.AreEqual(1, sut.NumberOfVotingCardsReceived);
            Assert.AreEqual(1, sut.NumberOfVotingCardsValidated);
            Assert.AreEqual(1, sut.NumberOfVotingCardNotReceived);

            Assert.AreEqual(4000, sut.VotingCardAmtIssured);
            Assert.AreEqual(2000, sut.VotingCardAmtReceived);
            Assert.AreEqual(2000, sut.VotingCardAmtNotReceived);

            Assert.AreEqual(50, sut.VotingCardAmtReceivedRate);
            Assert.AreEqual(50, sut.VotingCardAmtNotReceivedRate);


        }

        [TestMethod]
        public void AccumulateForCandidate_OneVotingCardVotedTheSameForEachCandidate()
        {
            //Create Voted BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;
            foreach (var item in card1.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            //Create Voted BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);

            //Action
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForCandidate(votingCards);

            //Assert
            foreach (var candidate in sut.CandidateLines)
            {
                Assert.AreEqual(1000, candidate.TotalVoting);
            }

        }

        [TestMethod]
        public void AccumulateForCandidate_TwoVotingCardVotedTheSameForEachCandidate()
        {
            //Create Voted BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;
            foreach (var item in card1.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            var card2 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card2.IsVoted = true;
            foreach (var item in card2.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            //Create Voted BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);
            votingCards.Add(card2);

            //Action
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForCandidate(votingCards);

            //Assert
            foreach (var candidate in sut.CandidateLines)
            {
                Assert.AreEqual(2000, candidate.TotalVoting);
            }
        }

        [TestMethod]
        public void AccumulateForCandidate_TwoVotingCardVotedTheSameForEachCandidateAndTheSecondIsNotVoted()
        {
            //Create Voted BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;
            foreach (var item in card1.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            var card2 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card2.IsVoted = false;
            foreach (var item in card2.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            //Create Voted BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);
            votingCards.Add(card2);

            //Action
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForCandidate(votingCards);

            //Assert
            foreach (var candidate in sut.CandidateLines)
            {
                Assert.AreEqual(1000, candidate.TotalVoting);
            }
        }

        [TestMethod]
        public void AccumulateForCandidate_TwoVotingCardVotedTheSameForEachCandidateAndTheSecondIsInvalid()
        {
            //Create Voted BODVotingCard            
            var card1 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card1.IsVoted = true;
            foreach (var item in card1.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            var card2 = new VotingCard(_shareHolder, _candidates, VotingCardType.BODVotingCard);
            card2.IsVoted = true;
            card2.IsInvalid = true;
            foreach (var item in card2.VotingCardLines)
            {
                item.VotingAmt = 1000;
            }

            //Create Voted BODVotingCard List
            var votingCards = new List<VotingCard>();
            votingCards.Add(card1);
            votingCards.Add(card2);

            //Action
            var sut = new VotingResultView(_candidates, VotingCardType.BODVotingCard);
            sut.AccumulateForCandidate(votingCards);

            //Assert
            foreach (var candidate in sut.CandidateLines)
            {
                Assert.AreEqual(1000, candidate.TotalVoting);
            }
        }



    }
}
