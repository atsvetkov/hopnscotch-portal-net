using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Hopnscotch.Portal.Web
{
    public static class IocConfig
    {
        private const string AssemblyPrefix = "Hopnscotch";

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.StartsWith(AssemblyPrefix)).ToArray();
            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<AutofacWebTypesModule>();

            // Change controller action parameter injection by changing web.config.
            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerHttpRequest();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}