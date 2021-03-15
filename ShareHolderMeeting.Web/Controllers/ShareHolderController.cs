using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Application.Common.Interfaces;
using Persistence;
using ShareHolderMeeting.Web.Models;

namespace ShareHolderMeeting.Web.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ShareHolderController : Controller
    {
        private readonly IShareHolderContext _context;

        public ShareHolderController(IShareHolderContext context)
        {
            _context = context;
        }

        public async Task<ViewResult> Index()
        {
            return View(await _context.ShareHolders.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //shareholderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

