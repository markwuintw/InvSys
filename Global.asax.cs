using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sys
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //RouteTable.Routes.RouteExistingFiles = true; // �����P�_�O�_�������ɮצs�b�A�����i��MVC���Ѥ��
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
