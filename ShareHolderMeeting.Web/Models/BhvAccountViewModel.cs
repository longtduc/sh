using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShareHolderMeeting.Web.Models
{
    public class UserRoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<RoleVM> Roles { get; set; }

        public UserRoleVM()
        {
            Roles = new List<RoleVM>();
        }
    }

    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}