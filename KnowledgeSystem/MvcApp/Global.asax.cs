using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.WebPages;
using MvcApp.ViewModels;
using Newtonsoft.Json;
using MvcApp.Infrastructure;
using System.Security.Principal;
using MvcApp.Controllers;
using Log.Interface;

namespace MvcApp
{
    /// <summary>
    /// Main class with methods on various cases
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Actions when app started
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Authentificate actions
        /// </summary>
        protected void Application_AuthenticateRequest()
        {
            if (HttpContext.Current.User != null)
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                    if (formsIdentity != null)
                    {
                        string userData = formsIdentity.Ticket.UserData;
                        var userInfo = userData.IsEmpty() ? new MvcUser() : JsonConvert.DeserializeObject<MvcUser>(userData);
                        var customIdentity = new CustomIdentity
                        {
                            IsAuthenticated = true,
                            Name = formsIdentity.Ticket.Name,
                            Email = userInfo.Email,
                            Id = userInfo.Id,
                            Roles = userInfo.Roles.ToArray(),
                        };
                        HttpContext.Current.User = new GenericPrincipal(customIdentity, userInfo.Roles.ToArray());
                    }
                }
        }

        /// <summary>
        /// Actions when error is occured
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (ReferenceEquals(exception,null)) return;

            Response.Clear();
            var routeData = new RouteData();            
            var httpErrorCode = (exception as HttpException)?.GetHttpCode();

            routeData.Values.Add("httpErrorCode", httpErrorCode);
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("exception", exception);

            var logger = (ILogger)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ILogger));
            IController controller = new ErrorController(logger);
            controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

            Response.End();
        }
    }
}
