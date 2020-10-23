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
            return _context.ShareHolders.Find(id);
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
    }
    public interface IShareHolderRepo : IRepository<ShareHolder> { };
}