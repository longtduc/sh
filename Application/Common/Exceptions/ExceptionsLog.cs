using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ExceptionsLog
    {
        private readonly IShareHolderContext _ctx;

        public ExceptionsLog(IShareHolderContext ctx)
        {
            _ctx = ctx;
        }

        public void SaveToLogTable(string message)
        {
            _ctx.ExceptionLogs.Add(new ExceptionLog(message));
            _ctx.SaveChanges();
        }
    }
}
