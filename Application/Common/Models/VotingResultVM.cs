using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Common.Models
{
    public class VotingResultVM
    {
        public int NumberOfVotingCardsIssured { get; set; } //
        public int NumberOfVotingCardsReceived { get; set; } //
        public int NumberOfVotingCardsValidated { get; set; } //
        public int NumberOfVotingCardNotReceived { get; set; } //Derived
        public int VotingCardAmtIssured { get; set; } //
        public int VotingCardAmtReceived { get; set; } //
        public int VotingCardAmtNotReceived { get; set; } //Derived

        public decimal VotingCardAmtIssuredRate { get; set; } //
        public decimal VotingCardAmtReceivedRate { get; set; } //
        public decimal VotingCardAmtNotReceivedRate { get; set; } //Derived

        public int ValidVotingCardAmtReceived { get; set; } //
        public decimal ValidVotingCardAmtReceivedRate { get; set; }

        public List<CandidateLine> CandidateLines { get; set; }
        public VotingResultVM()
        {
            CandidateLines = new List<CandidateLine>();
        }

        //used for create the list of candidates
        public VotingResultVM(List<Candidate> candidates, VotingCardType type)
        {
            CandidateLines = new List<CandidateLine>();
            foreach (var candidate in candidates)
            {
                if (candidate.IsValidForVotingCard(type))
                {
                    var line = new CandidateLine()
                    {
                        Id = candidate.Id,
                        Name = candidate.Name
                    };
                    CandidateLines.Add(line);
                }
            }
        }

        public void Accumulate(List<VotingCard> votingCards)
        {
            AccumulateForRoot(votingCards);
            AccumulateForCandidate(votingCards);
            CalculateRateForCandidate();
        }

        private void CalculateRateForCandidate()
        {
            foreach (var candidate in this.CandidateLines)
            {
                candidate.VotingRate = CalculateRate(candidate.TotalVoting, this.VotingCardAmtIssured);
            }

        }       

        public void AccumulateForRoot(List<VotingCard> votingCards)
        {
            foreach (var card in votingCards)
            {
                var availableAmt = card.NumberOfCandidates * card.NumberOfShares;

                NumberOfVotingCardsIssured += 1;
                NumberOfVotingCardsReceived += (card.IsVoted) ? 1 : 0;
                NumberOfVotingCardsValidated += (card.IsVoted && !card.IsInvalid) ? 1 : 0;

                VotingCardAmtIssured += availableAmt;
                VotingCardAmtReceived += (card.IsVoted) ? availableAmt : 0;
                ValidVotingCardAmtReceived += (card.IsVoted && !card.IsInvalid) ? availableAmt : 0;
            }

            NumberOfVotingCardNotReceived = NumberOfVotingCardsIssured - NumberOfVotingCardsReceived;
            VotingCardAmtNotReceived = VotingCardAmtIssured - VotingCardAmtReceived;
            VotingCardAmtIssuredRate = 100;

            VotingCardAmtReceivedRate = CalculateRate(VotingCardAmtReceived, VotingCardAmtIssured);
            ValidVotingCardAmtReceivedRate = CalculateRate(ValidVotingCardAmtReceived, VotingCardAmtIssured);
            VotingCardAmtNotReceivedRate = CalculateRate(VotingCardAmtNotReceived, VotingCardAmtIssured);

        }

        private decimal CalculateRate(decimal value, decimal total)
        {
            if (value == 0M)
                return 0M;
            if (total == 0)
                throw new ArgumentOutOfRangeException("Can not calculate rate because the denominator are equal zero!");
            return Math.Round((decimal)value / total * 100, 2);
        }

        public void AccumulateForCandidate(List<VotingCard> votingCards)
        {
            foreach (var card in votingCards)
            {
                if (card.IsVoted && !card.IsInvalid)
                    foreach (var votingLine in card.VotingCardLines)
                    {
                        foreach (var candidate in this.CandidateLines)
                        {
                            if (candidate.Id == votingLine.CandidateId)
                            {
                                candidate.TotalVoting += votingLine.VotingAmt;
                            }

                        }
                    }
            }

        }

        public class CandidateLine
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal  TotalVoting { get; set; }
            public decimal VotingRate { get; set; }
        }
    }


}