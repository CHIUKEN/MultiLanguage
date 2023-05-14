using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiLanguageWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Root",
            //    url: "",
            //    defaults: new
            //    {
            //        controller = "Base",
            //        action = "RedirectToLocalized"
            //    }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{culture}/{controller}/{action}/{id}",
                defaults: new
                {
                    culture = "zh-tw",
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                constraints: new { culture = "zh-tw|en-us" }
            );

            routes.MapRoute(
               name: "LocalizedDefault",
               url: "{lang}/{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" },
               constraints: new { lang = "zh-tw|en-us" }
           );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
