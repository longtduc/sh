using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Statement(string description)
        {
            if (String.IsNullOrEmpty(description))
                throw new InvalidOperationException("Description must be not empty!");
            this.Description = description;
        }

        public Statement(int id, string description)
        {
            if (String.IsNullOrEmpty(description))
                throw new InvalidOperationException("Description must be not empty!");
            this.Description = description;
            this.Id = id;
        }


    }
}