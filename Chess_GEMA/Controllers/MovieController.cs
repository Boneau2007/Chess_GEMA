using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chess_GEMA.Controllers
{

    /*
     * Hier werden die funktionalitaeten fetgelegt, wie wir mit Filmen umgehen wollen.
     * Ist derzeit noch nicht implementiert, folgt aber noch. Noetiges Model existiert bereits.
     * 
     */
    public class MovieController : ImageController
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }
    }
}