
using Application.Common.Interfaces;
using Domain.Common;
using ShareHolderMeeting.Web.CqsForCandidate;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Commands.CqsForCandidate
{
    public class CandidateCommandHandler
    {
        private readonly IShareHolderContext _context;
        public CandidateCommandHandler(IShareHolderContext context)
        {
            _context = context;
        }
        public CommandResult Handler(RemoveCandidateCommand cmd)
        {
            var id = cmd.CandidateId;
            var candidate = _context.Candidates.Find(id);
            if (candidate == null)
                return new CommandResult()
                {
                    Message = "Candidate does not exist=> Can not delete",
                    ReturnObj = candidate
                };
            try
            {
                _context.Candidates.Remove(candidate);
                _context.SaveChanges();
                return new CommandResult()
                {
                    ReturnObj = candidate,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public CommandResult Handler(CreateCandidateCommand cmd)
        {
            var candidate = cmd.Candidate;
            var _candidateValidator = new CandidateValidator();
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);

                return new CommandResult()
                {
                    ReturnObj = candidate,
                    Message = CoreHelper.MergeErrors(brokerRules),
                    Success = false
                };
            }
            //Try to insert
            try
            {
                _context.Candidates.Add(candidate);
                _context.SaveChanges();
                return new CommandResult()
                {
                    Success = true,
                    ReturnObj = candidate
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    ReturnObj = candidate,
                    Message = ex.Message
                };
            }

        }

        public CommandResult Handler(UpdateCandidateCommand cmd)
        {
            var candidate = cmd.Candidate;
            var _candidateValidator = new CandidateValidator();
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);

                return new CommandResult()
                {
                    ReturnObj = candidate,
                    Message = CoreHelper.MergeErrors(brokerRules),
                    Success = false
                };
            }
            try
            {
                //_context.Candidates.Attach(candidate);
                var entity = _context.Candidates.Find(cmd.Candidate.Id);

                entity.Name = cmd.Candidate.Name;
                _context.SaveChanges();
                return new CommandResult()
                {
                    Success = true,
                    ReturnObj = candidate
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    ReturnObj = candidate,
                    Message = ex.Message
                };
            }
        }
    }
}
