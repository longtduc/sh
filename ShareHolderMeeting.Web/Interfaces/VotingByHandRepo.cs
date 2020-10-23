using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace ShareHolderMeeting.Web.Interfaces
{
    public class VotingByHandRepo : IVotingByHandRepo
    {
        private readonly ShareHolderContext _context;


        public VotingByHandRepo(ShareHolderContext context)
        {
            this._context = context;
        }


        public IQueryable<VotingByHand> All
        {
            get { return _context.VotingByHands; }
        }

        public IQueryable<VotingByHand> AllIncluding(params Expression<Func<VotingByHand, object>>[] includeProperties)
        {
            IQueryable<VotingByHand> query = _context.VotingByHands;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public VotingByHand Find(int id)
        {
            return _context.VotingByHands.Find(id);
        }

        public void InsertOrUpdate(VotingByHand entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                _context.VotingByHands.Add(entity);
            }
            else
            {
                // Existing entity
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = _context.VotingByHands.Find(id);
            _context.VotingByHands.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateGraph(VotingByHand entity)
        {
            foreach (var line in entity.VotingByHandLines)
            {
                _context.Entry(line).State = System.Data.Entity.EntityState.Modified;
            }
        }

    }

    public interface IVotingByHandRepo : IRepository<VotingByHand>
    {
        void UpdateGraph(VotingByHand entity);
    }
}