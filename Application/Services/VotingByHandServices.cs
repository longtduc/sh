using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Services
{
    public class VotingByHandServices
    {

        private IShareHolderContext _context;

        public VotingByHandServices(IShareHolderContext context)
        {
            _context = context;
        }

        public List<VotingByHand> GetVotingByHands()
        {
            var results = _context.VotingByHands.Include("VotingByHandLines")
                    .Where(v=>v.ShareHolderId != null)
                    .ToList();
            return results;
        }

        public VotingByHand GetVotingByHand(int id)
        {
            var votingByHand = _context.VotingByHands.Include("VotingByHandLines")
                .Where(i => i.Id == id)
                .FirstOrDefault();

            return votingByHand;
        }
        public void GenerateVotingByHands()
        {
            RemoveVotingByHands();
            var shareHolders = _context.ShareHolders
                            .Where(m => m.StatusAtMeeting != StatusAtMeeting.Absent)
                            .ToList();

            var statements = _context.Statements.ToList();
            foreach (var sh in shareHolders)
            {
                CreateVotingByHand(sh, statements);
            }

            _context.SaveChanges();
        }

        public void CreateVotingByHand(ShareHolder shareHolder, List<Statement> statements)
        {
            var votingByHand = new VotingByHand(shareHolder, statements);
            _context.VotingByHands.Add(votingByHand);

        }

        public void RemoveVotingByHands()
        {
            var votingByHands = _context.VotingByHands.ToList();

            foreach (var item in votingByHands)
            {
                _context.VotingByHands.Remove(item);
            }
        }

        public void UpdateVoingByHand(VotingByHand input)
        {
            var entity = _context.VotingByHands
                            .Include("VotingByHandLines")
                            .Where(v => v.Id == input.Id)
                            .FirstOrDefault();
            if (entity == null)
                return;
            entity.Vote(input.VotingByHandLines);
            _context.SaveChanges();
        }

        public VotingByHandResultVM CreateVotingByHandResult()
        {
            var votingByHands = _context.VotingByHands.Include("VotingByHandLines")
                            .ToList();
            var result = new VotingByHandResultVM(votingByHands);
            return result;
        }


    }
}