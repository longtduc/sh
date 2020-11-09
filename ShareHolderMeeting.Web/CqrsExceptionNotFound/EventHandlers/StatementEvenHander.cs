using Application.Common.Interfaces;
using Domain.Entities;
using Persistence;
using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.CqrsExceptionNotFound
{
    public class StatementEvenHander
    {
        public void Handler(CreateStatementEvent @event)
        {
            using (var context = new ShareHolderContext())
            {
                context.Statements.Add(new Statement(@event.Description));
                context.SaveChanges();
            }
        }
    }
}