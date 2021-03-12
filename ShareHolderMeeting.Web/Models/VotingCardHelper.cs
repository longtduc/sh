using Application.Common.Models;
using Application.VotingCards;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class VotingCardHelper
    {
        public static VotingCardLine ToVotingCardLine(VotingCardLineDto dto)
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

        public static IList<VotingCardLine> ToVotingCardLines(ICollection<VotingCardLineDto> dto)
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

        public static VotingCardDto ToVotingCardDto(VotingCard votingCard)
        {
            var votingCardDto = new VotingCardDto()
            {
                Id = votingCard.Id,
                IsInvalid = votingCard.IsInvalid,
                AmtAlreadyVoted = votingCard.AmtAlreadyVoted,
                IsVoted = votingCard.IsVoted,
                NumberOfCandidates = votingCard.NumberOfCandidates,
                ShareHolderId = votingCard.ShareHolderId,
                NumberOfShares = votingCard.NumberOfShares,
                VotingCardType = votingCard.VotingCardType,
                VotingCardLines = new List<VotingCardLineDto>()
            };
            foreach (var item in votingCard.VotingCardLines)
            {
                var lineDto = new VotingCardLineDto()
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