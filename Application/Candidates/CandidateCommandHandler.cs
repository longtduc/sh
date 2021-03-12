
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common;
using Domain.Entities;
using System;

namespace Application.Candidates
{
    public class CandidateCommandHandler
    {
        private readonly IShareHolderContext _context;
        public CandidateCommandHandler(IShareHolderContext context)
        {
            _context = context;
        }
        public Result<Candidate> Handler(RemoveCandidateCommand cmd)
        {
            var id = cmd.CandidateId;
            var candidate = _context.Candidates.Find(id);
            if (candidate == null)
                return Result<Candidate>.Fail<Candidate>($"Candidate with id of {id} not found");
            try
            {
                _context.Candidates.Remove(candidate);
                _context.SaveChanges();
                return Result<Candidate>.OK<Candidate>(candidate);
               
            }
            catch (Exception ex)
            {
                return Result<Candidate>.Fail<Candidate>(ex.Message);
            }
        }

        public Result<Candidate> Handler(CreateCandidateCommand cmd)
        {
            var candidate = cmd.Candidate;
            var _candidateValidator = new CandidateValidator();
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);
                return Result.Fail<Candidate>(CoreHelper.MergeErrors(brokerRules));              
            }
            //Try to insert
            try
            {
                _context.Candidates.Add(candidate);
                _context.SaveChanges();
                return Result.OK<Candidate>(candidate);               
            }
            catch (Exception ex)
            {
                return Result<Candidate>.Fail<Candidate>(ex.Message);               
            }

        }

        public Result<Candidate> Handler(UpdateCandidateCommand cmd)
        {
            var candidate = cmd.Candidate;
            var _candidateValidator = new CandidateValidator();
            if (!_candidateValidator.IsValid(candidate))
            {
                var brokerRules = _candidateValidator.BrokenRules(candidate);

                return Result<Candidate>.Fail<Candidate>(CoreHelper.MergeErrors(brokerRules));
            }
            try
            {
                var entity = _context.Candidates.Find(cmd.Candidate.Id);
                entity.Name = cmd.Candidate.Name;

                _context.SaveChanges();

                return Result<Candidate>.OK<Candidate>(entity);
                
            }
            catch (Exception ex)
            {
                return Result<Candidate>.Fail<Candidate>(ex.Message);
            }
        }
    }
}
