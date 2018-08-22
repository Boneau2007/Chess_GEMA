using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class Articel
    {
        public int Id { get; set; }

        public string Titel { get; set; }
        public string SubTitel { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Gallery { get; set; }
        public string Path { get; set; }
    }
}