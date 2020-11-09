using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Domain.Entities;

namespace Application.Services
{
    public class VotingCardServices
    {

        private VotingCardRepo _votingCardRepo;
        //private CandidateRepo _candidateRepo;
        private ShareHolderContext _context;
        //private StatementRepo _statementRepo;

        public VotingCardServices()
        {
            _context = new ShareHolderContext();
            _votingCardRepo = new VotingCardRepo(_context);

            //_candidateRepo = new CandidateRepo(_context);
            //_statementRepo = new StatementRepo(_context);
        }


        public VotingCard GetVotingCard(int id)
        {
            var votingCard = _votingCardRepo.
               AllIncluding(m => m.VotingCardLines).
               Where(m => m.Id == id).
               FirstOrDefault();

            if (votingCard == null)
                throw new ArgumentException("VotingCard not found!");

            return votingCard;
        }

        public void GenerateVotingCards()
        {
            var shareHolders = _context.ShareHolders
                .Where(s => s.StatusAtMeeting != StatusAtMeeting.Absent)
                .ToList();
            var shareHolderService = new ShareHolderService();

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
            var votingCards = _votingCardRepo
                .AllIncluding(m => m.VotingCardLines)
                .Where(v => v.VotingCardType == type)
                .ToList();

            resultVM.Accumulate(votingCards);
            return resultVM;

        }

    }
}