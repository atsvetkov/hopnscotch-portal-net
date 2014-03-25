using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
//using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Hopnscotch.Portal.Import;

namespace Hopnscotch.Portal.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocConfig.Configure();
            
            //var importManager = DependencyResolver.Current.GetService<IAmoCrmImportManager>();
            //var amoCrmImportResult = importManager.Import(new AmoCrmImportOptions());
        }
    }
}
