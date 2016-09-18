﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp
{
    /// <summary>
    /// Class for routing configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Configure routes
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
