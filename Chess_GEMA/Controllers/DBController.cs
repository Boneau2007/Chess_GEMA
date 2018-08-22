using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chess_GEMA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess_GEMA.Controllers.Admin
{
    public class DBController : Controller
    {

        // create an object for our ApplicationDBContext
        protected ApplicationDbContext _context;

        public DBController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}