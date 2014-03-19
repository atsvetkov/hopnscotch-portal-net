using Autofac;

namespace Hopnscotch.Portal.Import
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AmoCrmEntityConverter>().As<IAmoCrmEntityConverter>();
            builder.RegisterType<AmoCrmImportManager>().As<IAmoCrmImportManager>();
        }
    }
}
