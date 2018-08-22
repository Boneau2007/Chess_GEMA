using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chess_GEMA.Models;
using Microsoft.AspNet.Identity;
using File = Chess_GEMA.Models.File;

namespace Chess_GEMA.Controllers
{
    public class FileController : Controller
    {

        private readonly string relativeServerPath = "/Content/";
        protected ApplicationDbContext _context;

        public FileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        // Methode um eine Datei nach ID zu löschen
        public ActionResult Delete(int id)
        {
            var file = _context.NewsFeed.SingleOrDefault(n => n.Id == id);
            if (file == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.NewsFeed.Remove(file);
            _context.SaveChanges();

            return RedirectToAction("Index", "NewsFeed");
        }

        // Methode um eine übergebene Datei zu editieren
        [HttpPost]
        public ActionResult Edit(File file)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", file);
            }

            var oldFile = _context.Files.Single(n => n.Id == file.Id);
            oldFile.Path = file.Path;
            oldFile.Name = file.Name;

            _context.SaveChanges();
            return RedirectToAction("Index", "File");
        }
        /*
         * Mit dieser Methode koennen wir beliebige Dateien auf dem Server speichern.
         * */
        // POST: File/Create
        [HttpPost]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var path = Server.MapPath(relativeServerPath);
                    var fileName = Path.GetFileName(file.FileName);

                   

                    if (fileName != null)
                    {
                        //Erstelle Verzeichniss falls nicht existend
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        path = Path.Combine(path, fileName);
                        file.SaveAs(path);
                        //Erstelle einen neuen Eintrag, wenn die Datei noch nicht in der Datenbank abgelegt ist
                        if (_context.Files.FirstOrDefault(s => s.Name == fileName) == null)
                        {
                            var relativePath = relativeServerPath + fileName;
                            var dbFile = new File
                            {

                                Path = relativePath,
                                Name = fileName,
                                Owner = User.Identity.GetUserName(),
                                Size = file.ContentLength
                            };

                            _context.Files.Add(dbFile);
                            _context.SaveChanges();
                        }
                        
                    }
                    
                }
            }
            return RedirectToAction("Index");
        }

        //GET: File/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}