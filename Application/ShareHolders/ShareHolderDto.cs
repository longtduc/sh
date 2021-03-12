using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.ShareHolders
{
    public class ShareHolderDto
    {
        public int ShareHolderId { get; set; }
        public string ShareHolderCode { get; set; }
        public string Name { get; set; }
        public int NumberOfShares { get; set; }
        public StatusAtMeeting StatusAtMeeting { get; set; }
    }
}