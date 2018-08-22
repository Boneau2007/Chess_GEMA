using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess_GEMA.Models
{
    public class Image : File
    {
        public string resolution { get; set; }

        public Gallery ReferenzGallery { get; set; }
    }
}