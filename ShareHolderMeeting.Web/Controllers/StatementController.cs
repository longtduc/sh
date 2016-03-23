using ShareHolderMeeting.Web.Interfaces;
using ShareHolderMeeting.Web.Models;
using ShareHolderMeeting.Web.Models.CoreServices;
using ShareHolderMeeting.Web.Services;
using ShareHolderMeeting.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    public class StatementController : Controller
    {
        private IStatementRepo _repo;
       
        private StatementService _svc;

        public StatementController()
            : this(new StatementRepo(),new StatementService())
        {

        }
        public StatementController(IStatementRepo repo, StatementService svc)
        {
            _repo = repo;          
            _svc = svc;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStatements()
        {
            var result = _repo.All.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Post(StatementVM vm)
        {
            dynamic result;

            //If ViewModel is invalid
            var errorMsg = GetModelErrors();        
            if (!String.IsNullOrEmpty(errorMsg))
            {
                result = new { Status = false, Message = errorMsg };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //Create
            try
            {
                var newId = _svc.Create(vm);
                result = new { Status = true, Id = newId };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Put(StatementVM vm)
        {
            //If ViewModel is invalid
            dynamic result;
            var errorMsg = GetModelErrors();            
            if (!String.IsNullOrEmpty(errorMsg))
            {
                result = new { Status = false, Message = errorMsg };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //Update 
            try
            {
                _svc.Update(vm);
                result = new { Status = true, Id = vm.Id };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            dynamic result;
            try
            {
                _svc.Delete(id);
                result = new { Status = true, Id = id };
            }
            catch (Exception ex)
            {
                result = new { Status = false, Message = ex.Message };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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