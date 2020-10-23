using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.CqrsExceptionNotFound
{
    public class CreateStatementCommand
    {
        public string Description { get; set; }
        public CreateStatementCommand(string desc)
        {
            this.Description = desc;
        }
    }
}