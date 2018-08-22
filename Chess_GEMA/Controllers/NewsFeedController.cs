/*
 * Controller für alle Funktionen, welche den NewsFeed betreffen.
 *
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Chess_GEMA.Models;
using Chess_GEMA.ViewModels;
using Microsoft.AspNet.Identity;

namespace Chess_GEMA.Controllers
{
    /*
     * Controllerklasse, welche die Funktionalitaet unseres NewsFeeds bereit stellt.
     * 
     * */
    public class NewsFeedController : Controller
    {

        private const byte standardTitelImageId = 1;

        private readonly ApplicationDbContext _context;
        
        //Methode in welcher der Zugriff auf den Datenbankkontext ermöglicht wird.
        public NewsFeedController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [AllowAnonymous]
        // Methode mit welcher der Inhalt der NewsFeed Tabelle aus der Datenbank ausgelesen werden kann.
        public ActionResult Index()
        {
            //Eager Loading des ImageFiles des NewsFeeds
            var newsFeed = _context.NewsFeed.Include(i => i.TitelImage).ToList();
            return View(newsFeed);
        }

       

        [HttpGet]
        public ActionResult Save()
        {
            return View();
        }
        
        //Methode mit welcher neue Einträge in der Datenbank gespeichert werden können.
        [HttpPost]
        public ActionResult Save(NewsFeed newsFeed, IEnumerable<HttpPostedFileBase> imageBase)
        {
            //Pruefe ob die Eingaben Valide sind, falls nicht wiederhole bis richtig
            if (!ModelState.IsValid)
            {
                var viewModel = new NewsFeedFormViewModel
                {
                    NewsFeed = newsFeed,
                    TitleImage = newsFeed.TitelImage
                };
                return View("Save", viewModel);
            }
            //Wenn NewsFeed nicht in der Datenbank ist, erstelle neuen, ansonsten uebernehme neue Daten
            if (newsFeed.Id == 0)
            {
                Add(newsFeed, imageBase);
            }
            else
            {
                //Gibt datenbankobject
                var newsInDb = _context.NewsFeed.Single(n => n.Id == newsFeed.Id);
                //Vom System vorgegeben
                newsInDb.Date = DateTime.Now;
                newsInDb.Edited = true;
                newsInDb.Moderator = User.Identity.GetUserName();
                //Frei waehlbar
                newsInDb.Titel = newsFeed.Titel;
                newsInDb.Text = newsFeed.Text;

                // Hier fuehren wir eine Context uebertrageung an einen anderen Controller durch
                var factory = DependencyResolver.Current.GetService<IControllerFactory>() ?? new DefaultControllerFactory();
                ImageController imageController = factory.CreateController(this.ControllerContext.RequestContext, "Image") as ImageController;
                RouteData route = new RouteData();
                route.Values.Add("action", "Test");

                ControllerContext imageContext = new ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, imageController);
                //Speichere Image in Datenbank 
                imageController.ControllerContext = imageContext;
                imageController.Create(imageBase);

                //Nehme das Image aus der Datenbank anhand des Namens und Rufe die View Create mit dem Image aus der Datenbank auf.
                var fileName = Path.GetFileName(imageBase.First().FileName);
                var dbImage = _context.Image.SingleOrDefault(n => n.Name == fileName);
                newsFeed.TitelImage = dbImage;
                _context.NewsFeed.Add(newsFeed);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "NewsFeed");
        }
        [HttpPost]
        public ActionResult Add(NewsFeed newsFeed, IEnumerable<HttpPostedFileBase> imageBase)
        {
            newsFeed.Date = DateTime.Now;
            newsFeed.Edited = false;
            newsFeed.Moderator = User.Identity.GetUserName();
            var factory = DependencyResolver.Current.GetService<IControllerFactory>() ?? new DefaultControllerFactory();
            ImageController imageController = factory.CreateController(this.ControllerContext.RequestContext, "Image") as ImageController;

            RouteData route = new RouteData();
            route.Values.Add("action", "Test");

            ControllerContext imageContext = new ControllerContext(new HttpContextWrapper(System.Web.HttpContext.Current), route, imageController);
            //Speichere Image in Datenbank 
            imageController.ControllerContext = imageContext;
            imageController.Create(imageBase);

            //Nehme das Image aus der Datenbank anhand des Namens und Rufe die View Create mit dem Image aus der Datenbank auf.
            var fileName = Path.GetFileName(imageBase.First().FileName);
            var dbImage = _context.Image.SingleOrDefault(n => n.Name == fileName);
            newsFeed.TitelImage = dbImage;
            _context.NewsFeed.Add(newsFeed);
            _context.SaveChanges();

            return RedirectToAction("Index", "NewsFeed");

        }
        //Methode mit welcher ein Bestehender Eintrag bearbeitet werden kann.
        [HttpPost]
        public ActionResult Edit(NewsFeed newsFeed)
        {
            //OldNews
            if (!ModelState.IsValid)
            {
                return View("Edit", newsFeed);
            }

            var oldNews = _context.NewsFeed.Single(n => n.Id == newsFeed.Id);
            //Hiermit werden die alten Einträge neu gesetzt und der Eintrag Edited auf "true" gesetzt
            oldNews.Titel = newsFeed.Titel;
            oldNews.Text = newsFeed.Text;
            oldNews.Date = DateTime.Now;
            oldNews.Edited = true;
            
            
            _context.SaveChanges();
            return RedirectToAction("Index", "NewsFeed");
        }

        //Methode mit welcher die alten Einträge an die Edit Seite übergeben werden, um sie dort anzeigen zu können.
        public ActionResult Edit(int id)
        {
            var oldNews = _context.NewsFeed.Single(n => n.Id == id);
            return View(oldNews);
        }
        
        //Methode mit welcher einzelne Einträge aus der Datenbank gelöscht werden können.
        public ActionResult Delete(int id)
        {
            var newsFeed = _context.NewsFeed.SingleOrDefault(n => n.Id == id);
            if (newsFeed == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.NewsFeed.Remove(newsFeed);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "NewsFeed");
        }
        
    }
}
