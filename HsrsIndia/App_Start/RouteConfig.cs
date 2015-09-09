using HsrsIndia.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HsrsIndia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //generic URLs
            routes.Add("CommonPathRoute",
                            new CommonPathRoute(
                                        "{generic_se_name}",
                                        new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                                        );

            routes.Add("CommonPathRoute1",
                new CommonPathRoute(
                            "{generic_se_name}/{se_name}",
                            new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                            );

            routes.Add("CommonPathRoute2",
               new CommonPathRoute(
                           "{generic_se_name}/{se_name}/{se_namelocation}",
                           new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Onlinebooking",
                url: "{controller}/{action}/{ObType}/{id}/{price}",
                defaults: new { controller = "Home", action = "Index", ObType = UrlParameter.Optional, id = UrlParameter.Optional, price = UrlParameter.Optional }
            );
        }
    }
}