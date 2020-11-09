using Application.Common.Models;
using Domain.Entities;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class VotingCardHelper
    {
        public static VotingCardLine ToVotingCardLine(VotingCardLineVM dto)
        {
            if (dto == null)
                throw new InvalidOperationException();

            return new VotingCardLine()
            {
                Id = dto.Id,
                CandidateId = dto.CandidateId,
                CandidateName = dto.CandidateName,
                VotingAmt = dto.VotingAmt,
                VotingCardId = dto.VotingCardId
            };
        }

        public static IList<VotingCardLine> ToVotingCardLines(ICollection<VotingCardLineVM> dto)
        {
            if (dto == null)
                throw new InvalidOperationException();

            IList<VotingCardLine> VotingCardLines = new List<VotingCardLine>();
            foreach (var item in dto)
            {
                VotingCardLines.Add(ToVotingCardLine(item));
            }
            return VotingCardLines;
        }

        public static VotingCardVM ToVotingCardDto(VotingCard votingCard)
        {
            var votingCardDto = new VotingCardVM()
            {
                Id = votingCard.Id,
                IsInvalid = votingCard.IsInvalid,
                AmtAlreadyVoted = votingCard.AmtAlreadyVoted,
                IsVoted = votingCard.IsVoted,
                NumberOfCandidates = votingCard.NumberOfCandidates,
                ShareHolderId = votingCard.ShareHolderId,
                NumberOfShares = votingCard.NumberOfShares,
                VotingCardType = votingCard.VotingCardType,
                VotingCardLines = new List<VotingCardLineVM>()
            };
            foreach (var item in votingCard.VotingCardLines)
            {
                var lineDto = new VotingCardLineVM()
                {
                    Id = item.Id,
                    CandidateId = item.CandidateId,
                    CandidateName = item.CandidateName,
                    VotingAmt = item.VotingAmt,
                    VotingCardId = item.VotingCardId
                };
                votingCardDto.VotingCardLines.Add(lineDto);
            }

            return votingCardDto;

        }

       


    }
}