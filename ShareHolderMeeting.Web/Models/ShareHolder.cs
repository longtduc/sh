using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class ShareHolder
    {
        public int ShareHolderId { get; set; }
        public string ShareHolderCode {get;set;}
        public string Name {get;set;}       
        public int NumberOfShares {get;set;}
        public StatusAtMeeting StatusAtMeeting { get; set; }

    }

    public enum StatusAtMeeting
    {
        Absent = 0,
        Attended = 1,
        Delegated = 2
    }
}