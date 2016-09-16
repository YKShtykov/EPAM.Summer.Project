using System.Web.Mvc;
using Log.Interface;
using System;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("",Name ="Home")]
        public ActionResult Index()
        {
            new Exception();
            return View();
        }        
    }
}