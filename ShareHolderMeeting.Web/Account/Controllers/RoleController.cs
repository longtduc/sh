using BHV.Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BHV.Account.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class RoleController : Controller
    {
        //
        private UserManager<ApplicationUser> _userManager;

        private RoleManager<IdentityRole> _roleManager;

        public RoleController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        public ActionResult Index()
        {
            return View("~/Account/Views/Role/Index.cshtml");
        }

        public JsonResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(string roleName)
        {
            object result = null;
            var action = _roleManager.Create(new IdentityRole() { Name = roleName });
            if (action.Succeeded)
            {
                var newRole = _roleManager.FindByName(roleName);
                result = new { Status = true, Message = "", ReturnObject = newRole };
            }
            else
            {
                result = new { Status = false, Message = action.Errors.First() };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string roleId)
        {
            dynamic result = new { status = false };
            var roleToRemove = _roleManager.FindById(roleId);
            var action = _roleManager.Delete(roleToRemove);
            if (action.Succeeded)
            {
                result = new { Status = true, Message = "", ReturnObject = roleToRemove };
            }
            else
            {
                result = new { Status = false, Message = action.Errors.First() };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}