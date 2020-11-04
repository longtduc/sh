﻿using ShareHolderMeeting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.ViewModel
{
    public class ShareHolderVM
    {
        public int ShareHolderId { get; set; }
        public string ShareHolderCode { get; set; }
        public string Name { get; set; }
        public int NumberOfShares { get; set; }
        public StatusAtMeeting StatusAtMeeting { get; set; }
    }
}