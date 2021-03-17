using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Application.Common.Interfaces;
using Domain.Entities;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize]
    
    public class CandidateController : Controller
    {
        public CandidateController(IShareHolderContext context)
        {
            _context = context;
        }
        
        private IShareHolderContext _context { get; }

        public async Task<ViewResult> Index(int type)
        {
            ViewBag.Type = type;
            var result = _context.Candidates.Where(c => c.CandidateType == (CandidateType)type).ToListAsync();

            return View(await result);
        }
     
    }
}

