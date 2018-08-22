using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chess_GEMA.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public ApplicationUser Answerer { get; set; }

        public Question ReferenzQuestion { get; set; }

        [AllowHtml]
        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public int PositivRating { get; set; }
        public int NegativeRating { get; set; }
        public DateTime Date { get; set; }
        public bool StarAnswer { get; set; }
        public IEnumerable<Answer> Comments { get; set; }
    }
}