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
    public class ShareHolderRepository : IShareHolderRepo
    {
        ShareHolderContext context;
        public ShareHolderRepository()
        {
            context = new ShareHolderContext();
        }

        public ShareHolderRepository(ShareHolderContext context)
        {
            this.context = context;
        }

        public IQueryable<ShareHolder> All
        {
            get { return context.ShareHolders; }
        }

        public IQueryable<ShareHolder> AllIncluding(params Expression<Func<ShareHolder, object>>[] includeProperties)
        {
            IQueryable<ShareHolder> query = context.ShareHolders;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ShareHolder Find(int id)
        {
            return context.ShareHolders.Find(id);
        }

        public void InsertOrUpdate(ShareHolder entity)
        {
            if (entity.ShareHolderId == default(int))
            {
                // New entity
                context.ShareHolders.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = context.ShareHolders.Find(id);
            context.ShareHolders.Remove(entity);

        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
    public interface IShareHolderRepo : IRepository<ShareHolder>  { };
}