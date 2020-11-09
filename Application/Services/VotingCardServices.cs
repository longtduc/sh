using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Services
{
    public class VotingCardServices
    {

        //private CandidateRepo _candidateRepo;
        private IShareHolderContext _context;
        //private StatementRepo _statementRepo;

        public VotingCardServices(IShareHolderContext context)
        {
            _context = context;

            //_candidateRepo = new CandidateRepo(_context);
            //_statementRepo = new StatementRepo(_context);
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
        public VotingResultVM CreateVotingResultVM(VotingCardType type)
        {
            //Create VotingResultVM
            var candidates = _context.Candidates.ToList();
            var resultVM = new VotingResultVM(candidates, type);

            //Accumulate from VotingCards
            var votingCards = _context.VotingCards.Include("VotingCardLines")
                .Where(v => v.VotingCardType == type && v.ShareHolderId != null)
                .ToList();

            resultVM.Accumulate(votingCards);
            return resultVM;

        }

    }
}