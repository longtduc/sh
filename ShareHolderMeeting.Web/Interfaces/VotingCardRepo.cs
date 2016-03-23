using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;


namespace ShareHolderMeeting.Web.Interfaces
{
    public class VotingCardRepo:IVotingCardRepo
    {
         ShareHolderContext context;
        public VotingCardRepo():this(new ShareHolderContext())
        {
            
        }

        public VotingCardRepo(ShareHolderContext context)
        {
            this.context = context;
        }
        public IQueryable<VotingCard> All
        {
            get { return context.VotingCards; }
        }

        public IQueryable<VotingCard> AllIncluding(params Expression<Func<VotingCard, object>>[] includeProperties)
        {
            IQueryable<VotingCard> query = context.VotingCards;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public VotingCard Find(int id)
        {
            return context.VotingCards.Find(id);
        }

        public void InsertOrUpdate(VotingCard entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                context.VotingCards.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = context.VotingCards.Find(id);
            context.VotingCards.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public void UpdateGraph(VotingCard entity)
        {
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;           

            foreach (var line in entity.VotingCardLines)
            {
                if (line.Id == default(int))
                    context.Entry(line).State = System.Data.Entity.EntityState.Added;
                else
                    context.Entry(line).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
    public interface IVotingCardRepo : IRepository<VotingCard> 
    {
        void UpdateGraph(VotingCard entity);
    }
}