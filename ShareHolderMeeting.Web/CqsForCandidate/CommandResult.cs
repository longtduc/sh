﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareHolderMeeting.Web.CqsForCandidate
{
    public class CommandResult
    {
        public CommandResult()
        {
            Success = false;
        }
        public bool Success { get; set; }
        public object ReturnObj { get; set; }
        public string Message { get; set; }
    }
}
