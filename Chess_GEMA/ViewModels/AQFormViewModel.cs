using System.Collections.Generic;
using Chess_GEMA.Models;

namespace Chess_GEMA.ViewModels
{
    public class AQFormViewModel
    {
        public Question Question { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}