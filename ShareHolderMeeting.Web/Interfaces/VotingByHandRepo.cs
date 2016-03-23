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
    public class VotingByHandRepo:IVotingByHandRepo
    {
        private ShareHolderContext context;
        public VotingByHandRepo():this(new ShareHolderContext())
        {
            
        }

        public VotingByHandRepo(ShareHolderContext context)
        {
            this.context = context;
        }


        public IQueryable<VotingByHand> All
        {
            get { return context.VotingByHands ; }
        }

        public IQueryable<VotingByHand> AllIncluding(params Expression<Func<VotingByHand, object>>[] includeProperties)
        {
            IQueryable<VotingByHand> query = context.VotingByHands;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public VotingByHand Find(int id)
        {
            return context.VotingByHands.Find(id);
        }

        public void InsertOrUpdate(VotingByHand entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                context.VotingByHands.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity =context.VotingByHands.Find(id);
            context.VotingByHands.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateGraph(VotingByHand entity)
        {
            foreach (var line in entity.VotingByHandLines)
            {
                context.Entry(line).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Dispose()
        {
            
        }
    }

    public interface IVotingByHandRepo : IRepository<VotingByHand> 
    {
        void UpdateGraph(VotingByHand entity);
    }
}