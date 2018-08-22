using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class NewsFeed
    {
        public int Id { get; set; }

        [Required]
        public string Titel { get; set; }

        public DateTime Date { get; set; }

        public string Moderator { get; set; }
        
        [Required]
        public string Text { get; set; }

        public Boolean Edited { get; set; }
        
        public Image TitelImage { get; set; }
       
        public Gallery GalerieImages { get; set; }
    }
}