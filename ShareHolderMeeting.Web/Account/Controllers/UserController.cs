using BHV.Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BHV.Account.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        public ActionResult Index()
        {
            return View("~/Account/Views/User/Index.cshtml");
        }

        [HttpGet]
        public JsonResult GetAddableRoles(string userId)
        {
            var allRole = _roleManager.Roles.Select(r => new { r.Id, r.Name, r.Users }).ToList(); ;
            var rolesAddable = new List<RoleVM>(); ;
            foreach (var role in _roleManager.Roles.ToList())
            {
                if (!role.Users.Any(u => u.UserId == userId))
                {
                    rolesAddable.Add(new RoleVM() { Id = role.Id, Name = role.Name });
                }
            }
            return Json(rolesAddable, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(string userId, string roleName)
        {
            dynamic result = new { Status = false };
            try
            {
                _userManager.RemoveFromRole(userId, roleName);
                result = new { Status = true, Message = "" };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddRole(string userId, string roleId)
        {
            dynamic result = new { Status = false };
            try
            {
                var addedRole = _roleManager.Roles
                                            .Where(r => r.Id == roleId)
                                            .Select(rr => new { rr.Id, rr.Name })
                                            .FirstOrDefault();
                _userManager.AddToRole(userId, addedRole.Name);
                result = new { Status = true, Message = "", ReturnObject = addedRole };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers()
        {
            var vm = GetUsersWithRoles();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        private List<UserRoleVM> GetUsersWithRoles()
        {
            var vm = new List<UserRoleVM>();
            //var users = _context.Users.ToList();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var ur = new UserRoleVM();
                ur.Id = user.Id;
                ur.Name = user.UserName;
                foreach (var role in user.Roles)
                {
                    var r = _roleManager.Roles.Where(rd => rd.Id == role.RoleId).FirstOrDefault();
                    ur.Roles.Add(new RoleVM() { Id = r.Id, Name = r.Name });
                }
                vm.Add(ur);
            }
            return vm;
        }

        public ActionResult DeleteUser(string userId)
        {
            var userToRemove = _userManager.FindById(userId);
            var action = _userManager.Delete(userToRemove);
            object result = null; ;
            if (action.Succeeded)
            {
                result = new { Status = true, Message = "", ReturnObject = userToRemove };
            }
            else
            {
                result = new { Status = false, Message = action.Errors.First() };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}