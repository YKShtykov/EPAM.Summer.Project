using System.Web;
using System.Web.Mvc;
using MvcApp.Infrastructure;

namespace MvcApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAllErrorAttribute());
        }
    }
}
