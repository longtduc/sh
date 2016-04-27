using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class CandidateFile
    {
        public int Id { get; set; }
        public string FileName  { get; set; }
        public string ContentType  { get; set; }

        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}