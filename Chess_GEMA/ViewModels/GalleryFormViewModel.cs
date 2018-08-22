using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chess_GEMA.Models;

namespace Chess_GEMA.ViewModels
{
    public class GalleryFormViewModel
    {
        public Gallery Gallery { get; set; }
        public IList<Image> Images { get; set; }
    }
}