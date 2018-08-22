using System;
using System.Collections;
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
    public class GalleryController : Controller
    {
        private readonly string relativeServerPath = "/Content/Images/";

        private readonly ApplicationDbContext _context;

        public GalleryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Methode um einen Gallerie Index View mit übergebenem Datenbankinhalt zu erzeugen
        public ActionResult Index()
        {
            var gallerys = _context.Gallery.OrderByDescending(d => d.Date).Include(i => i.TitelImage).ToList();
            return View(gallerys);
        }

        // Methode um einen Gallerieeintrag nach ID zu löschen, 
        // loescht jedoch nicht die Bilder, nur die Gallery. 
        // Dies wurde aus designtechnischen gruenden so entschieden.
        public ActionResult Delete(int id)
        {
            var gallery = _context.Gallery.SingleOrDefault(n => n.Id == id);
            if (gallery == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var images = _context.Image.Include(i => i.ReferenzGallery).ToList();

            foreach (var image in images)
            {
                var refGallery = image.ReferenzGallery;
                if (refGallery.Id == id)
                {
                    image.ReferenzGallery = null;
                }
            }
                // Löschen des Eintrags aus der Datenbank & anschließendes speichern
                _context.Gallery.Remove(gallery);
            _context.SaveChanges();

            return RedirectToAction("Index", "Gallery");
        }

        // Methode mit welcher ein neuer Gallerieeintrag erstellt wird
        [HttpPost]
        public ActionResult Create(Gallery gallery, IEnumerable<HttpPostedFileBase> imageBase)
        {
            gallery.Title = gallery.Title;
            gallery.Owner = User.Identity.GetUserName();
            gallery.Date = DateTime.Now;

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
            gallery.TitelImage = dbImage;

            _context.Gallery.Add(gallery);
            _context.SaveChanges();
            return RedirectToAction("Index", "Gallery");
        }

        // Methode welche einen View von Create zurück gibt
        public ActionResult Create()
        {
            return View();
        }
        
        // Methode welche einen Gallerieeintrag per ID nimmt und es als View an die Create Methode der Gallerie weiter gibt.
        public ActionResult Edit(int id)
        {
            var gallery = _context.Gallery.SingleOrDefault(n => n.Id == id);
            if (gallery == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return View("Create", gallery);
        }


        public ActionResult Show(int id)
        {
            var gallery = _context.Gallery.SingleOrDefault(i => i.Id == id);

            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        public ActionResult Detail(int id)
        {

            IList<Image> album = new List<Image>();

            //Laedt alle Antowrten aus der Datenbank
            var images = _context.Image.Include(i => i.ReferenzGallery).ToList();

            foreach (var image in images)
            {
                var refGallery = image.ReferenzGallery;
                if (refGallery != null)
                {
                    if (refGallery.Id == id)
                    {
                        album.Add(image);
                    }
                }
            }
            return View(album);
        }

        public ActionResult AddToGallery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToGallery(int id, HttpPostedFileBase image)
        {
            var gallery = _context.Gallery.Include(i => i.AlbumImages).SingleOrDefault(i => i.Id == id);

            if (image.ContentLength > 0)
            {
                var extensions = Path.GetExtension(image.FileName).ToLower();
                var serverMapPath = Server.MapPath(relativeServerPath);
                var fileName = Path.GetFileName(image.FileName);
                if (fileName != null && (extensions == ".jpg" || extensions == ".jpeg" || extensions == ".png"))
                {
                    //Erstelle Verzeichniss falls nicht existend
                    if (!Directory.Exists(serverMapPath))
                    {
                        Directory.CreateDirectory(serverMapPath);
                    }

                    var serverPath = Path.Combine(serverMapPath, fileName);
                    image.SaveAs(serverPath);
                    if (_context.Image.FirstOrDefault(s => s.Name == fileName) == null)
                    {
                        var relativePath = relativeServerPath + fileName;
                        var filestream = System.IO.File.ReadAllBytes(serverPath);
                        var dbImage = new Image()
                        {
                            Path = relativePath,
                            Name = fileName,
                            Owner = User.Identity.GetUserName(),
                            Size = filestream.Length

                        };

                        dbImage.ReferenzGallery = gallery;
                        _context.Files.Add(dbImage);
                        _context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
