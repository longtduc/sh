using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShareHolderMeeting.Web.Interfaces
{
   public interface IRepository<T>: IDisposable where T: class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);        
        T Find(int id);
        void InsertOrUpdate(T entity);
        void Delete(int id);
        void Save();
    }
}
