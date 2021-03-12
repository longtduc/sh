using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ShareHolders;
using Domain.Entities;
using System;
using System.Data;
using System.Linq;

namespace Application.VotingCards
{
    public class VotingCardServices
    {

        private IShareHolderContext _context;

        public VotingCardServices(IShareHolderContext context)
        {
            _context = context;
        }


        public VotingCard GetVotingCard(int id)
        {
            var votingCard = _context.VotingCards.Include("VotingCardLines")
                .Where(m => m.Id == id)
                .FirstOrDefault();

            if (votingCard == null)
                throw new ArgumentException("VotingCard not found!");

            return votingCard;
        }

        public void GenerateVotingCards(ShareHolderService service)
        {
            var shareHolders = _context.ShareHolders
                .Where(s => s.StatusAtMeeting != StatusAtMeeting.Absent)
                .ToList();
            var shareHolderService = service;

            foreach (var shareHolder in shareHolders)
            {
                var shareHolderId = shareHolder.ShareHolderId;
                var currentState = shareHolder.StatusAtMeeting;
                //Remove all exiting Voting Cards
                shareHolderService.ChangeShareHolderStatus(shareHolderId, (int)StatusAtMeeting.Absent);
                if (shareHolder.StatusAtMeeting == StatusAtMeeting.Attended
                    || shareHolder.StatusAtMeeting == StatusAtMeeting.Delegated)
                    shareHolderService.ChangeShareHolderStatus(shareHolderId, (int)currentState);
            }

        }
        public VotingResultView CreateVotingResultVM(VotingCardType type)
        {
            //Create VotingResultVM
            var candidates = _context.Candidates.ToList();
            var resultVM = new VotingResultView(candidates, type);

            //Accumulate from VotingCards
            var votingCards = _context.VotingCards.Include("VotingCardLines")
                .Where(v => v.VotingCardType == type)
                .ToList();

            resultVM.Accumulate(votingCards);
            return resultVM;

        }

    }
}