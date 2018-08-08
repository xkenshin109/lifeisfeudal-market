using System.Web;
using System.Web.Mvc;

namespace life_is_feudal_your_own
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
