using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Services
{
    public class VotingByHandServices
    {
     
        private List<Statement> _statements;
        private UoWVotingByHand _uowVotingByHand;
        private ShareHolderContext _context;

        public VotingByHandServices()
        {
            _context = new ShareHolderContext();
            _uowVotingByHand = new UoWVotingByHand(_context);
            _statements = _uowVotingByHand.Statements.All.ToList();
        }

        public List<VotingByHand> GetVotingByHands()
        {
            var results = _uowVotingByHand.VotingByHands
                    .AllIncluding(v => v.VotingByHandLines)
                    .ToList();        
            return results;
        }

        public VotingByHand GetVotingByHand(int id)
        {
            var votingByHand = _uowVotingByHand.VotingByHands
                .AllIncluding(c => c.VotingByHandLines)
                .Where(i=>i.Id == id)
                .FirstOrDefault();
       
            return votingByHand;
        }
        public void GenerateVotingByHands()
        {
            RemoveVotingByHands();
            var shareHolders = _uowVotingByHand.ShareHolders.All
                .Where(m=>m.StatusAtMeeting != StatusAtMeeting.Absent)
                .ToList();
          
            foreach (var sh in shareHolders)
            {
                CreateVotingByHand(sh);              
            }
            Save();
        }

        public void CreateVotingByHand(ShareHolder shareHolder)
        {
            var votingByHand = new VotingByHand(shareHolder, _statements);
            _uowVotingByHand.VotingByHands.InsertOrUpdate(votingByHand);
           
        }
      
        public void RemoveVotingByHands()
        {
            var votingByHands = _uowVotingByHand.VotingByHands.All.ToList();

            foreach (var item in votingByHands)
            {
                _uowVotingByHand.VotingByHands.Delete(item.Id);
            }           
        }

        public void UpdateVoingByHand(VotingByHand entity)
        {
            var repo = new VotingByHandRepo(new ShareHolderContext());
            repo.UpdateGraph(entity);
            repo.Save();
        }

        public VotingByHandResultVM CreateVotingByHandResult()
        {
            var votingByHands = _uowVotingByHand.VotingByHands
                            .AllIncluding(v=>v.VotingByHandLines)
                            .ToList();
            var result = new VotingByHandResultVM(votingByHands);
            return result;
        }

        public void Save()
        {
            _uowVotingByHand.VotingByHands.Save();
        }
    }
}