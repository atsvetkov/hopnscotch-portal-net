using Autofac;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Data.Helpers;

namespace Hopnscotch.Portal.Data
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<LessonRepository>().As<ILessonRepository>();
            //builder.RegisterType<AttendanceRepository>().As<IAttendanceRepository>();

            builder.RegisterType<RepositoryFactories>().As<IRepositoryFactories>();
            builder.RegisterType<RepositoryProvider>().As<IRepositoryProvider>();
            builder.RegisterType<AttendanceUow>().As<IAttendanceUow>();
            builder.RegisterType<AttendanceDbContextFactory>().As<IAttendanceDbContextFactory>();
        }
    }
}
