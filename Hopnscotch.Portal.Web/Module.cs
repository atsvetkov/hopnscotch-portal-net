using Autofac;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Web
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Config>().As<IConfig>();
        }
    }
}