using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chess_GEMA.Models;
using Microsoft.AspNet.Identity;

namespace Chess_GEMA.Controllers.FAQ
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Answers
        public ActionResult Index()
        {
            var answers = _context.Answer.Include(a => a.Answerer).ToList();
            return View(answers);
        }

        // Methode welche eine Antwort über die ID als View an Details zurückgibt
        public ActionResult Details(int? id)
        {
            var answers = _context.Answer.Include(a => a.Answerer).SingleOrDefault(i => i.Id == id);

            if (answers == null)
            {
                return HttpNotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public ActionResult Create()
        {
            return View();
        }

        // Methode mit welcher eine Antwort gespeichert wird. 
        [HttpPost]
        public ActionResult Create(Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", answer);
            }
            answer.Date = DateTime.Now;
            var currentUser = User.Identity.GetUserId();
            answer.Answerer = _context.Users.SingleOrDefault(i => i.Id == currentUser);
            answer.ReferenzQuestion = _context.Question.SingleOrDefault(i => i.Id == answer.Id);

            // Fügt den Eintrag der Datenbank hinzu und speichert dies.
            _context.Answer.Add(answer);
            _context.SaveChanges();
            return RedirectToAction("Details", "Questions", answer.ReferenzQuestion);
        }

        // Methode mit welcher eine Antwort über die ID gelöscht wird
        public ActionResult Delete(int? id)
        {
            var answer = _context.Answer.SingleOrDefault(i => i.Id == id);

            if (answer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            // Entfernt den Datenbankeintrag und speichert die Änderung
            _context.Answer.Remove(answer);
            _context.SaveChanges();
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        //Makiert Antworten als besondere Hilfe fuer den Fragesteller
        public ActionResult SetStarAnswer(int id)
        {
            //Welcher User greift zu
            var currentUser = _context.Users.SingleOrDefault(i => i.UserName == HttpContext.User.Identity.Name);
           //Welche Antwort soll veraendert werden
            var answer = _context.Answer.Include(i => i.ReferenzQuestion).SingleOrDefault(i => i.Id == id);
            //Wie lautete die Frage dazu
            var refQuestion = answer.ReferenzQuestion;
            //Sind Zugreifender User und Eigentuemer der Frage gleich
            if (currentUser.UserName.Equals(refQuestion.Questioner.UserName)) { 
                answer.StarAnswer = !answer.StarAnswer;
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "Questions", refQuestion);
        }
    }
}
