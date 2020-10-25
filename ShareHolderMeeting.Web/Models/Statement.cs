using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class Statement
    {
        public int Id { get; set; }
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                ValidateDecription(value);
                _description = value;
            }
        }

        public Statement()
        {

        }
        public Statement(string description)
        {
            ValidateDecription(description);
            _description = description;
        }

        private static void ValidateDecription(string description)
        {
            if (String.IsNullOrEmpty(description))
                throw new InvalidOperationException("Description must be not empty!");
        }     

    }
}