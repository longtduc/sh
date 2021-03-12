using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.VotingByHands
{
    public class VotingByHandResultView
    {
        public int TotalNumberOfShares { get; set; }

        public ICollection<StatementResult> StatementResults { get; set; }

        public VotingByHandResultView(List<VotingByHand> votingByHands)
        {
            InitializeStatementResults(votingByHands);
            Accumulate(votingByHands);
            CalculateRateForStatements();

        }

        private void CalculateRateForStatements()
        {
            foreach (var statement in this.StatementResults)
            {
                statement.YesRate = CalculateRate(statement.AmtOfSharesYes);
                statement.NoRate = CalculateRate(statement.AmtOfSharesNo);
                statement.OtherRate = CalculateRate(statement.AmtOfSharesOther);         
            }
        }

        private decimal CalculateRate(decimal value)
        {
            return Math.Round((decimal)value / this.TotalNumberOfShares * 100, 2);
        }

        private void Accumulate(List<VotingByHand> votingByHands)
        {
            foreach (var voting in votingByHands)
            {
                this.TotalNumberOfShares += voting.NumberOfShares;
                foreach (var line in voting.VotingByHandLines)
                {
                    var statement = this.StatementResults
                        .Where(l => l.Id == line.StatementId)
                        .FirstOrDefault();
                    if (line.VotingOption == VotingOption.Yes)
                    {
                        statement.AmtOfSharesYes += voting.NumberOfShares;
                    }
                    else if (line.VotingOption == VotingOption.No)
                    {
                        statement.AmtOfSharesNo += voting.NumberOfShares;
                    }
                    else
                        statement.AmtOfSharesOther += voting.NumberOfShares;

                }

            }

        }
        private void InitializeStatementResults(List<VotingByHand> votingByHands)
        {
            this.StatementResults = new List<StatementResult>();
            var firstVotingByHand = votingByHands.FirstOrDefault();
            foreach (var item in firstVotingByHand.VotingByHandLines)
            {
                var lineResult = new StatementResult()
                {
                    Id = item.StatementId,
                    Description = item.StatementDesc
                };
                StatementResults.Add(lineResult);
            }
        }
        public class StatementResult
        {
            public int Id { get; set; }
            public string Description { get; set; }

            public int AmtOfSharesYes { get; set; }
            public decimal YesRate { get; set; }

            public decimal AmtOfSharesNo { get; set; }
            public decimal NoRate { get; set; }

            public int AmtOfSharesOther { get; set; }
            public decimal OtherRate { get; set; }


        }
    }
}