using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShawInterviewExercise.Web
{
    public class RouteConfig
    {
        /* Map routes such that show names are seen and treated 
         * as if they have their own controllers.
         * e.g. localhost/rookieblue --> localhost/shows/details?showName=rookieblue
         *      localhost/rookieblue/video --> localhost/shows/video?showName=rookieblue */
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*--- Root behaviour ---*/
            routes.MapRoute(
                name: "Root",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShawInterviewExercise.Web.Controllers" }
            );

            /*--- Controller default behaviours ---*/
            routes.MapRoute(
                name: "Home Default",
                url: "Home/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShawInterviewExercise.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Shows Default",
                url: "Shows/{action}/{id}",
                defaults: new { controller = "Shows", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin Default",
                url: "Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );            

            /*--- TV Show Names behaviour ---*/
            routes.MapRoute(
                name: "Show Video",
                url: "{showName}/video",
                defaults: new { Controller = "Shows", action = "Video" }
            );

            routes.MapRoute(
                name: "{showName} Default",
                url: "{showName}",
                defaults: new { Controller = "Shows", action = "Details" }
            );

            /*--- Catch-all default route ---*/
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShawInterviewExercise.Web.Controllers" }
            );
        }
    }
}