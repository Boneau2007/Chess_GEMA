using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        
        [StringLength(255)]
        public string Title { get; set; }

        public string Owner { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public Image TitelImage { get; set; }
        
        public IList<Image> AlbumImages { get; set; }
    }
}