using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using Domain.Entities;

namespace Persistence
{
    public class VotingCardRepo : IVotingCardRepo
    {
        private readonly ShareHolderContext _context;


        public VotingCardRepo(ShareHolderContext context)
        {
            _context = context;
        }
        public IQueryable<VotingCard> All
        {
            get { return _context.VotingCards.Where(v=>v.ShareHolderId != null); }
        }

        public IQueryable<VotingCard> AllIncluding(params Expression<Func<VotingCard, object>>[] includeProperties)
        {
            IQueryable<VotingCard> query = _context.VotingCards.Where(v => v.ShareHolderId != null);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public VotingCard Find(int id)
        {
            return _context.VotingCards
                .Include(v=>v.VotingCardLines).Where(x=>x.Id == id).FirstOrDefault();                        
        }

        public void InsertOrUpdate(VotingCard entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                _context.VotingCards.Add(entity);
            }
            else
            {
                // Existing entity
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = _context.VotingCards.Find(id);

            if (entity.ShareHolderId == null)
                throw new ArgumentOutOfRangeException();
            _context.VotingCards.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateGraph(VotingCard entity)
        {
            if (_context != null)
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            foreach (var line in entity.VotingCardLines)
            {
                if (line.Id == default(int))
                    _context.Entry(line).State = System.Data.Entity.EntityState.Added;
                else
                    _context.Entry(line).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
    public interface IVotingCardRepo : IRepository<VotingCard>
    {
        void UpdateGraph(VotingCard entity);        
    }
}