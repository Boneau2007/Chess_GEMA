using System.Web.Mvc;
using System.Linq;
using Chess_GEMA.Controllers.Admin;
using Chess_GEMA.Models;

namespace Chess_GEMA.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [AllowAnonymous]

        // GET: NewsFeed
        public ActionResult Index()
        {
            //Eager Loading des ImageFiles des NewsFeeds
            var news = _context.NewsFeed.ToList();
            return View(news);
        }

       
        // Methode in Welche ein Viewbag an die About Seite übergeben wird
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. (SC. GEMA St. Ingbert)";

            return View();
        }

        public void EditContactPicture() { }
        public void DeleteContactPicture() { }

    }
}