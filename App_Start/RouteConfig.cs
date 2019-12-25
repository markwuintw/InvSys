using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sys
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //只要網址出現任何.axd結尾的網址，不論後面接的所有路徑為何，皆會被視為跳過網址路由的類型

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "InvAccounts", action = "login", id = UrlParameter.Optional }

            //,
            //constraints:new
            //{
            //    id=@"\d+"  //此例之，附加條件為id必須全為數字才符合此路由
            //}
            );
        }
    }
}
