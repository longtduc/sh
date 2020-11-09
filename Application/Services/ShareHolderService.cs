using Domain.Entities;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Services
{
    public class ShareHolderService
    {
        //private ShareHolderRepo _shareHolderRepo;
        private IShareHolderContext _context;

        public ShareHolderService(IShareHolderContext context)
        {
            _context = context;
            //_shareHolderRepo = new ShareHolderRepo(_context);
        }
        public void ChangeShareHolderStatus(int shareHolderId, int newStatus)
        {
            ShareHolder sh = _context.ShareHolders
                        .Include("VotingCards")
                        .Include("VotingByHands")
                        .Where(s=>s.ShareHolderId == shareHolderId)
                        .FirstOrDefault();
            var newStatusInEnum = (StatusAtMeeting)newStatus;
            if (sh == null || sh.StatusAtMeeting == newStatusInEnum)
                return;

            if ((sh.StatusAtMeeting == StatusAtMeeting.Attended && newStatusInEnum == StatusAtMeeting.Delegated)
                || sh.StatusAtMeeting == StatusAtMeeting.Delegated && newStatusInEnum == StatusAtMeeting.Attended)
            {
                sh.StatusAtMeeting = newStatusInEnum;
                _context.SaveChanges();
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
            _context.SaveChanges();
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