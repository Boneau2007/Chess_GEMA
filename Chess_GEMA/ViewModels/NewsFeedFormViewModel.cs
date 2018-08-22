using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chess_GEMA.Models;

namespace Chess_GEMA.ViewModels
{
    public class NewsFeedFormViewModel
    {
        public IEnumerable<Image> Images{ get; set; }
        public NewsFeed NewsFeed { get; set; }
        public Image TitleImage { get; set; }
    }
}