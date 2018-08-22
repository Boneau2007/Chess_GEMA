using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Chess_GEMA.Models
{
    public class Question
    {
        public int Id { get; set; }

        public ApplicationUser Questioner { get; set; }

        public IList<ApplicationUser> Subscriber { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(".*\\?", ErrorMessage = "Frage muss mit \"?\" enden.")]
        public string Topic { get; set; }

        [AllowHtml]
        [StringLength(512, ErrorMessage = "Text darf macimal 512 Zeichen Enthalten")]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public IList<Answer> Answers { get; set; }

        public Answer StarAnswer { get; set; }

        public bool Answered { get; set; }
    }
}