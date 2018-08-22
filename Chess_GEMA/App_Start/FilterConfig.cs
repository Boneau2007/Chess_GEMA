using System.Web.Mvc;

namespace Chess_GEMA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           filters.Add(new AuthorizeAttribute());
        }
    }
}
