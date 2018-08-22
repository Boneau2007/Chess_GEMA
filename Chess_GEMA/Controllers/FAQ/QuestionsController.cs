using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Chess_GEMA.Models;
using Chess_GEMA.ViewModels;
using Microsoft.AspNet.Identity;

namespace Chess_GEMA.Controllers.FAQ
{
    [AllowAnonymous]
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public QuestionsController()
        {
            _context = new ApplicationDbContext();
        }

        //Listet alle Fragen und Anzahl der dazugehörigen Antowrten
        // GET: Questions
        public ActionResult Index()
        {
            IList<AQFormViewModel> viewModels = new List<AQFormViewModel>();

            //Laedt alle Fragen und Fragesteller(Eager Loading) aus der Datenbank
            var questions = _context.Question.Include(m => m.Questioner).ToList();

            //Fuer jede Frage in der Datenbank wird eine Liste von Antorten 
            //dazu erstellt und zusammen mit der Frage einer Neuen View hinzugefuegt
            foreach (var question in questions)
            {

                IList<Answer> refAnswers = GetAnswers(question); 
                
                var viewModel = new AQFormViewModel
                {
                    Question = question,
                    Answers = refAnswers
                };

                if (viewModel.Answers == null)
                {
                    viewModel.Answers = new List<Answer>();
                }

                viewModels.Add(viewModel);
            }

            
            var viewModelList = new ListAQFormViewModel
            {
                AqFormViewModels = viewModels
            };
            return View(viewModelList);
        }

        //Gibt die Frage mit den Antorten aus
        // GET: Questions/Details/5
        public ActionResult Details(int id)
        {
            var question = _context.Question
                .Include(q => q.Questioner)
                .SingleOrDefault(i => i.Id == id);

            if (question == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AQFormViewModel
            {
                Question = question,
                Answers = GetAnswers(question)
            };
            return View(viewModel);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        //Erstellt ein Frage-Object fuer die Datenbank aus dem uebergebenem POST der Clientseite
        // POST: Questions/Create
        [HttpPost]
        public ActionResult Create(Question question)
        {
            //Using Request wird auf Validitaet geprueft, 
            //falls Invalide wird die Anfrage wieder auf mit Gesendeten Informationen zurueckgesendet
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                question.Questioner = _context.Users.SingleOrDefault(s => s.UserName.Equals(userName));
                question.Date = DateTime.Now;

                _context.Question.Add(question);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            var  question = _context.Question.SingleOrDefault(i => i.Id == id);

            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        public ActionResult Edit(Question question)
        {
            //OldQuestion
            if (!ModelState.IsValid)
            {
                return View("Edit", question);
            }

            var oldQuestion = _context.Question.Single(n => n.Id == question.Id);

            //Ersetzt den alten Inhalt mit neuem
            oldQuestion.Topic = question.Topic;
            oldQuestion.Text = question.Text;
            oldQuestion.Date = question.Date;
            oldQuestion.Answered = question.Answered;
            oldQuestion.Answers = question.Answers;

            _context.SaveChanges();
            return RedirectToAction("Index", "NewsFeed");
        }
        
        //Loescht die Frage
        // GET: Questions/Delete/5
        public ActionResult Delete(int id)
        {
            // Löscht alle Antworten die auf die Frage Referenziert sind
            var questionsWithAnswers = _context.Question.Include(a => a.Answers).SingleOrDefault(i => i.Id == id);
            _context.Answer.RemoveRange((questionsWithAnswers.Answers));
           
            // Löscht die Referenz auf den Fragesteller
            var questionForSub = _context.Question.Include(a => a.Subscriber).SingleOrDefault(i => i.Id == id);
            questionForSub.Subscriber = null;

            //Loescht die Frage
            _context.Question.Remove(questionForSub);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        //Hilfsmethode um alle Antworten auf eine Frage zu bekommen
        private IList<Answer> GetAnswers(Question question)
        {
            IList<Answer> refAnswers = new List<Answer>();

            //Laedt alle Antowrten aus der Datenbank
            var answers = _context.Answer.ToList();

            foreach (var answer in answers)
            {
                var refQuestion = answer.ReferenzQuestion;
                if (refQuestion == question)
                    refAnswers.Add(answer);
            }
            return refAnswers;
        }

        //Hilfsmethode um die Frage zu schließen
        public ActionResult SwitchAnswered(int id)
        {
            var question = _context.Question.SingleOrDefault(i => i.Id == id);
            question.Answered = !question.Answered;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // Hilfsmethode, erhoeht das Rating der Frage. Ist auf jeweils einen Benutzer begrenzt.
        // Sprich es kann nicht mehrfach gerated werden
        public ActionResult LikeQuestions(int id)
        {
            var question = _context.Question.Include(s => s.Subscriber).SingleOrDefault(i => i.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }

            //Hat der derzeitige User bereits diese Frage Geliked
            var currentUser = _context.Users.SingleOrDefault(i => i.UserName == HttpContext.User.Identity.Name);
            if (!question.Subscriber.Contains(currentUser))
            {
                question.Likes++;
                question.Subscriber.Add(currentUser);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", question);
        }
    }
}
