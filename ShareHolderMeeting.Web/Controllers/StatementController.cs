using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Statements;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class StatementController : Controller
    {
        private readonly IShareHolderContext _context;

        private readonly StatementService _svc;

        public StatementController(IShareHolderContext repo, StatementService svc)
        {
            _context = repo;            
            _svc = svc;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetStatements()
        {
            var result = _context.Statements.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public JsonResult Put(StatementDto vm) //Create a statement
        {            
            //If ViewModel is invalid
            var errorMsg = GetModelErrors();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                return Json(ControllerHelper.TranslateErrorToClient(Result.Fail(errorMsg)), JsonRequestBehavior.AllowGet);
            }

            Result<int> result = _svc.Create(vm);
            return Json(ControllerHelper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //Update a Statement
        public JsonResult Post(StatementDto vm)
        {
            //If ViewModel is invalid
            var errorMsg = GetModelErrors();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                return Json(ControllerHelper.TranslateErrorToClient(Result.Fail(errorMsg)), JsonRequestBehavior.AllowGet);
            }

            Result<int> result = _svc.Update(vm);
            return Json(ControllerHelper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }      
     

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Result<int> result = _svc.Delete(id);
            return Json(ControllerHelper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }

        private string GetModelErrors()
        {
            var errorMsg = "";
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMsg += ";" + error.ErrorMessage;
                    }
                }
            }
            return errorMsg;
        }
    }
}