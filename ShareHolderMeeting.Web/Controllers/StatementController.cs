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
        private IStatementRepo _repo;
       
        private StatementService _svc;

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
            Result result = null;

            //If ViewModel is invalid
            var errorMsg = GetModelErrors();        
            if (!String.IsNullOrEmpty(errorMsg))
            {
                
                return Json(TranslateErrorToClient(new Result(false,errorMsg, 0)), JsonRequestBehavior.AllowGet);
            }

            result = _svc.Create(vm);            
            return Json(TranslateErrorToClient(result) , JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //Update a Statement
        public JsonResult Post(StatementVM vm)
        {
            //If ViewModel is invalid
            Result result = null;
            var errorMsg = GetModelErrors();            
            if (!String.IsNullOrEmpty(errorMsg))
            {
                result = new Result(false, errorMsg, vm.Id);
                return Json(TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
            }           

            result = _svc.Update(vm);            
            return Json(TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }
        
        private dynamic TranslateErrorToClient(Result result)
        {
            return new { Status = result.Success, Message = result.ErrorMessage, Id = result.Id }; 
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Result result = _svc.Delete(id);                
            return Json(TranslateErrorToClient(result), JsonRequestBehavior.AllowGet);
        }

        public string GetModelErrors()
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