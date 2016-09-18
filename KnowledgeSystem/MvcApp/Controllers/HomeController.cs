using System.Web.Mvc;
using System;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Initial page controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns initial page
        /// </summary>
        /// <returns></returns>
        [Route("",Name ="Home")]
        public ActionResult Index()
        {
            new Exception();
            return View();
        }        
    }
}