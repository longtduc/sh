using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ShareHolderMeeting.Web.Interfaces
{
    public class StatementRepo:IStatementRepo
    {
        private ShareHolderContext context;
        public StatementRepo():this(new ShareHolderContext())
        {
            
        }

        public StatementRepo(ShareHolderContext context)
        {
            this.context = context;
        }
        public IQueryable<Statement> All
        {
            get { return context.Statements; }
        }

        public IQueryable<Statement> AllIncluding(params Expression<Func<Statement, object>>[] includeProperties)
        {
            IQueryable<Statement> query = context.Statements;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Statement Find(int id)
        {
            return context.Statements.Find(id);
        }

        public void InsertOrUpdate(Statement entity)
        {
            if (entity.Id == default(int))
            {
                // New entity
                context.Statements.Add(entity);
            }
            else
            {
                // Existing entity
                context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var entity = context.Statements.Find(id);
            context.Statements.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            
        }
    }

    public interface IStatementRepo : IRepository<Statement> { }
}