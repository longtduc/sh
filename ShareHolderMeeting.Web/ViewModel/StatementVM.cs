using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.ViewModel
{
    public class StatementVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }    
    }
}