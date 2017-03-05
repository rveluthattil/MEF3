using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace MEF3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var pluginFolders = new List<string>();
            var plugins = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules")).ToList();

            plugins.ForEach(s =>
            {
                var di = new DirectoryInfo(s);
                pluginFolders.Add(di.Name);
            }
            );
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootStrapper.Compose(pluginFolders);
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
            ViewEngines.Engines.Add(new CustomViewEngine(pluginFolders));
        }
    }
}
