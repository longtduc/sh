using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.CqrsExceptionNotFound
{
    public class CreateStatementEvent
    {
        public string Description { get; set; }
        public CreateStatementEvent(string description)
        {
            this.Description = description;
        }
    }
}