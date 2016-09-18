using System;
using System.Web.Mvc;
using Log.Interface;

namespace MvcApp.Controllers
{
    /// <summary>
    /// Class for error logic. Returns various error pages
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        /// <summary>
        /// Create error controller
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// returns error page
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="httpErrorCode"></param>
        /// <returns></returns>
        public ActionResult Index(Exception exception, int httpErrorCode=0)
        {
            if (httpErrorCode == 404)
                return View("Error404");
            logger.LogError(exception);
            return View();
        }
    }
}