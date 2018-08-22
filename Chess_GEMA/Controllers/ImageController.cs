using System.Collections.Generic;
using Chess_GEMA.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Chess_GEMA.Controllers
{

    [AllowAnonymous]
    public class ImageController : Controller
    {
        private readonly string relativeServerPath = "/Content/Images/";

        protected ApplicationDbContext _context;

        public ImageController()
        {
            _context = new ApplicationDbContext();
        }
        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Pictures
        public ViewResult Index()
        {
            var messages = _context.Image.ToList();

            return View(messages);
        }
        
        
        // Methode zum Löschen eines Bildes, über die ID
        public ActionResult Delete(int id)
        {
            var image = _context.Image.SingleOrDefault(n => n.Id == id);
            if (image == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Image.Remove(image);
            _context.SaveChanges();

            return RedirectToAction("Index", "Image");
        }

        // Methode mit welcher ein View eines Bildes erzeugt wird
        public ActionResult Show(IEnumerable<Image> images)
        {
            return View(images);
        }


        //GET: ~/Image/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int Id)
        {
            var imageInDb = _context.Image.SingleOrDefault(i => i.Id == Id);
            return View(imageInDb);
        }
        /*
         * Nimmt lediglich Bilder des Formates .jpg, .png und .jpeg entgegen und speichert diese in der Datenbank 
         **/
        [HttpPost]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> images)
        {
            foreach (var image in images)
            {
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
                            _context.Files.Add(dbImage);
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Image");
        }
    }
}