using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ShareHolderMeeting.Web.Interfaces
{
    public class ShareHolderRepo : IShareHolderRepo
    {
        private readonly ShareHolderContext _context;

        public ShareHolderRepo(ShareHolderContext context)
        {
            this._context = context;
        }

        public IQueryable<ShareHolder> All
        {
            get { return _context.ShareHolders; }
        }

        public IQueryable<ShareHolder> AllIncluding(params Expression<Func<ShareHolder, object>>[] includeProperties)
        {
            IQueryable<ShareHolder> query = _context.ShareHolders;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ShareHolder Find(int id)
        {
            return _context.ShareHolders
                    .Include(i=>i.VotingCards)
                    .Include(j=>j.VotingByHands)
                    .Where(s=>s.ShareHolderId==id)
                    .FirstOrDefault();
        }

        public void InsertOrUpdate(ShareHolder entity)
        {
            if (entity.ShareHolderId == default(int))
            {
                // New entity
                _context.ShareHolders.Add(entity);
            }
            else
            {
                // Existing entity
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = _context.ShareHolders.Find(id);
            if (entity != null)
                _context.ShareHolders.Remove(entity);

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void RemoveAllVotingCardsAndVotingByHands(ShareHolder entity)
        {
            if (_context != null)
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //Note: Error message "Collection was modified; enumeration operation may not execute"
            //Fix: entity.VotingCards => entity.VotingCards.ToList()
            foreach (var item in entity.VotingCards.ToList()) 
            {
                _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }

            foreach (var item in entity.VotingByHands.ToList())
            {
                _context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
        }
    }
    public interface IShareHolderRepo : IRepository<ShareHolder>
    {
        void RemoveAllVotingCardsAndVotingByHands(ShareHolder sh);
    };
}