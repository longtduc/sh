using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Services
{
    public class ShareHolderService
    {
        private ShareHolderRepo _shareHolderRepo;
        private ShareHolderContext _context;

        public ShareHolderService()
        {
            _context = new ShareHolderContext();
            _shareHolderRepo = new ShareHolderRepo(_context);
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
            var candidates = _context.Candidates.ToList();
            sh.CreateVotingCards(candidates);

            var statements = _context.Statements.ToList();
            sh.CreateVotingByHands(statements);
        }
    }
}