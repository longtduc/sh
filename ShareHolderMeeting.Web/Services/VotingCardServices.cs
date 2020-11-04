using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace ShareHolderMeeting.Web.Services
{
    public class VotingCardServices
    {

        private VotingCardRepo _votingCardRepo;
        private ShareHolderRepo _shareHolderRepo;
        private CandidateRepo _candidateRepo;
        private VotingByHandRepo _votingByHandRepo;
        //private UoWvotingCard _uowVotingCard;
        private ShareHolderContext _context;
        private StatementRepo _statementRepo;

        public VotingCardServices()
        {
            _context = new ShareHolderContext();
            _votingCardRepo = new VotingCardRepo(_context);
            _votingByHandRepo = new VotingByHandRepo(_context);
            _shareHolderRepo = new ShareHolderRepo(_context);

            _candidateRepo = new CandidateRepo(_context);
            _statementRepo = new StatementRepo(_context);
            //_uowVotingCard = new UoWvotingCard(_context);
        }



        public void ChangeShareHolderStatus(int shareHolderId, int newStatus)
        {
            ShareHolder sh = _shareHolderRepo.Find(shareHolderId);
            var newStatusInEnum = (StatusAtMeeting)newStatus;
            if (sh == null || sh.StatusAtMeeting == newStatusInEnum)
                return;

            if ((sh.StatusAtMeeting == StatusAtMeeting.Attended && newStatusInEnum == StatusAtMeeting.Delegated)
                || sh.StatusAtMeeting == StatusAtMeeting.Delegated && newStatusInEnum == StatusAtMeeting.Attended)
            {
                sh.StatusAtMeeting = newStatusInEnum;
                _shareHolderRepo.Save();
                return;
            }

            //Update Status
            sh.StatusAtMeeting = newStatusInEnum;
            StatusAtMeeting newStateInEnum = (StatusAtMeeting)newStatus;

            switch (newStateInEnum)
            {
                case StatusAtMeeting.Absent:
                    sh.RemoveAllVotingCardsAndVotingByHands();
                    break;
                case StatusAtMeeting.Attended:
                    CreateVotingCardsAndVotingByHands(sh);
                    break;
                case StatusAtMeeting.Delegated:
                    CreateVotingCardsAndVotingByHands(sh);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _shareHolderRepo.Save();
        }

        private void CreateVotingCardsAndVotingByHands(ShareHolder sh)
        {
            //Create VotingCard for BOD,BOS
            var candidates = _candidateRepo.All.ToList();
            sh.CreateVotingCards(candidates);

            var statements = _statementRepo.All.ToList();
            sh.CreateVotingByHands(statements);
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
            var shareHolders = _shareHolderRepo.All
                .Where(s => s.StatusAtMeeting != StatusAtMeeting.Absent)
                .ToList();

            foreach (var shareHolder in shareHolders)
            {
                var shareHolderId = shareHolder.ShareHolderId;
                var currentState = shareHolder.StatusAtMeeting;
                ChangeShareHolderStatus(shareHolderId, (int)StatusAtMeeting.Absent);
                ChangeShareHolderStatus(shareHolderId, (int)currentState);
            }

        }
        public VotingResultVM CreateVotingResultVM(VotingCardType type)
        {
            //Create VotingResultVM
            var candidates = _candidateRepo.All
                    .ToList();
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