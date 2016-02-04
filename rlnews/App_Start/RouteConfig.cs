using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace rlnews
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "AboutRoute",
                 "about",
                 new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                 "ContactRoute",
                 "contact",
                 new { controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
                 "NewsRoute",
                 "news",
                 new { controller = "News", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
