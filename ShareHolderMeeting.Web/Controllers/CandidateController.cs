using System;
using System.Collections.Generic;
using System.Linq;
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

        public ViewResult Index(int type)
        {
            ViewBag.Type = type;
            return View(_context.Candidates.Where(c => c.CandidateType == (CandidateType)type));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

