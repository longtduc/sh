using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Services
{
    public class VotingCardServices
    {

        private VotingCardRepo _votingCardRepo;
        private ShareHolderRepo _shareHolderRepo;
        private CandidateRepo _candidateRepo;
        private UoWvotingCard _uowVotingCard;
        private ShareHolderContext _context;
        //private UoWVotingByHand _uoWVotingByHand;
        public VotingCardServices()
        {
            _context = new ShareHolderContext();
            _votingCardRepo = new VotingCardRepo(_context);
            _shareHolderRepo = new ShareHolderRepo(_context);
            _candidateRepo = new CandidateRepo(_context);
            _uowVotingCard = new UoWvotingCard(_context);
            //_uoWVotingByHand = new UoWVotingByHand(_context);
        }

        public void ChangeShareHolderStatus(int shareHolderId, int newStatus)
        {
            ShareHolder sh = _uowVotingCard.ShareHolders.Find(shareHolderId);
            if (sh == null || sh.StatusAtMeeting == (StatusAtMeeting)newStatus)
                return;

            //Update Status
            sh.StatusAtMeeting = (StatusAtMeeting)newStatus;
            StatusAtMeeting newStateInEnum = (StatusAtMeeting)newStatus;

            switch (newStateInEnum)
            {
                case StatusAtMeeting.Absent:
                    //Delete Voting Card when registering shareholders
                    DeleteVotingCard(shareHolderId);
                    break;
                case StatusAtMeeting.Attended:
                    //Create VotingCard for BOD,BOS
                    CreateVotingCard(sh, VotingCardType.BOSVotingCard);
                    CreateVotingCard(sh, VotingCardType.BODVotingCard);
                    break;
                case StatusAtMeeting.Delegated:
                    //Create VotingCard for BOD,BOS
                    CreateVotingCard(sh, VotingCardType.BODVotingCard);
                    CreateVotingCard(sh, VotingCardType.BOSVotingCard);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            sh.StatusAtMeeting = newStateInEnum;
            _uowVotingCard.ShareHolders.InsertOrUpdate(sh);

            Commit();
        }

        public void CreateVotingCard(ShareHolder shareHolder, VotingCardType type)
        {

            //Find VotingCard
            var votingCard = _votingCardRepo
                .All
                .Where(m => m.ShareHolderId == shareHolder.ShareHolderId)
                .FirstOrDefault();

            if (votingCard == null) //Create if not existed
            {
                var candidates = _candidateRepo.All.ToList();
                votingCard = new VotingCard(shareHolder, candidates, type);
                _uowVotingCard.VotingCards.InsertOrUpdate(votingCard);
            }


        }

        public void DeleteVotingCard(int shareHolderId)
        {
            var votingCards = _votingCardRepo.All.
                Where(m => m.ShareHolderId == shareHolderId);

            if (votingCards == null)
                return;
            foreach (var card in votingCards)
            {
                _uowVotingCard.VotingCards.Delete(card.Id);
            }

            var votingByHands = _vo

        }

        //private void UpdateStatusForShareHolder(int shareHolderId, int newStatus)
        //{
        //    var sh = _uowVotingCard.ShareHolders.Find(shareHolderId);
        //    sh.StatusAtMeeting = (StatusAtMeeting)newStatus;
        //    _uowVotingCard.ShareHolders.InsertOrUpdate(sh);
        //}
        public void Commit()
        {
            _uowVotingCard.Commit();
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