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
    public class CandidateRepo:ICandidateRepo
    {
        private ShareHolderContext context;

        public CandidateRepo():this(new ShareHolderContext())
        {

        }

        public CandidateRepo(ShareHolderContext  context)
        {
            this.context = context;
        }
        public IQueryable<Candidate> All
        {
            get { return context.Candidates; }
        }

        public IQueryable<Candidate> AllIncluding(params Expression<Func<Candidate, object>>[] includeProperties)
        {
            IQueryable<Candidate> query = context.Candidates;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Candidate Find(int id)
        {
            return context.Candidates.Find(id);
        }

        public void InsertOrUpdate(Candidate entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                context.Candidates.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = context.Candidates.Find(id);
            context.Candidates.Remove(entity);
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
    public interface ICandidateRepo : IRepository<Candidate> { }
}