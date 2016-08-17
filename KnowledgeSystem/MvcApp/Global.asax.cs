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

namespace MvcApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

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
    }
}
