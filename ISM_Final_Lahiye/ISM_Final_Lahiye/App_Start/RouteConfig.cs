using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ISM_Final_Lahiye
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); 
            routes.MapRoute(
                "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { area = "Adminpanel", controller = "LoginLogout", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "ISM_Final_Lahiye.Areas.Adminpanel.Controllers" }
            ).DataTokens.Add("area", "Adminpanel");
        }
    }
}
