using ShareHolderMeeting.Web.Common;
using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Services;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class StatementController : Controller
    {
        private readonly IStatementRepo _repo;

        private readonly StatementService _svc;

        public StatementController(IStatementRepo repo, StatementService svc)
        {
            _repo = repo;
            _svc = svc;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetStatements()
        {
            var result = _repo.All.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public JsonResult Put(StatementVM vm) //Create a statement
        {            
            //If ViewModel is invalid
            var errorMsg = GetModelErrors();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                return Json(Helper.TranslateErrorToClient(Result.Fail(errorMsg)), JsonRequestBehavior.AllowGet);
            }

            Result<int> result = _svc.Create(vm);
            return Json(Helper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //Update a Statement
        public JsonResult Post(StatementVM vm)
        {
            //If ViewModel is invalid
            var errorMsg = GetModelErrors();
            if (!String.IsNullOrEmpty(errorMsg))
            {
                return Json(Helper.TranslateErrorToClient(Result.Fail(errorMsg)), JsonRequestBehavior.AllowGet);
            }

            Result<int> result = _svc.Update(vm);
            return Json(Helper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }

       
     

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Result<int> result = _svc.Delete(id);
            return Json(Helper.TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
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