using System;
using System.Web.Mvc;
using Log.Interface;

namespace MvcApp.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }

        public ActionResult Index(Exception exception, int httpErrorCode=0)
        {
            if (httpErrorCode == 404)
                return View("Error404");
            logger.LogError(exception);
            return View();
        }
    }
}