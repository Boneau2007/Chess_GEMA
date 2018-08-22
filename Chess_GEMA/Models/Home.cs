using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class Home
    {
        public int Id { get; set; }

        [Required]
        public string Titel { get; set; }

        public string Moderator { get; set; }

        [Required]
        public string Text { get; set; }

        public NewsFeed NewsFeed { get; set; }

        public int NewsFeedId { get; set; }
    }
}