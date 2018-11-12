using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using testAdv.Controllers;

namespace testAdv
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "LogIn",
                url: "LogIn",
                defaults: new { controller = "Home", action = "LogIn", valide = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Error",
                url: "Error",
                defaults: new { controller = "Home", action = "Error" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
