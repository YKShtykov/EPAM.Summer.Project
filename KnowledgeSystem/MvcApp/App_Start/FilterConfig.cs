using System.Web;
using System.Web.Mvc;
using MvcApp.Infrastructure;

namespace MvcApp
{
    /// <summary>
    /// Class for filter configuration
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register filtres
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleAllErrorAttribute());
        }
    }
}
