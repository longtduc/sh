using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class Statement
    {
        public int Id { get; set; }

        private string description;
        public string Description
        {
            get { return this.description; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new InvalidOperationException("Description must be not empty!");
                description = value;
            }
        }
    }
}