using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Data.Helpers;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class AttendanceUow : IAttendanceUow
    {
        private readonly IAttendanceDbContextFactory dbContextFactory;
        private readonly IRepositoryProvider repositoryProvider;
        private AttendanceDbContext context { get; set; }

        public AttendanceUow(IRepositoryProvider repositoryProvider, IAttendanceDbContextFactory dbContextFactory)
        {
            this.repositoryProvider = repositoryProvider;
            context = dbContextFactory.GetContext();
            repositoryProvider.DbContext = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<Lesson> Lessons
        {
            get
            {
                return repositoryProvider.GetRepositoryForEntityType<Lesson>();
            }
        }

        public IRepository<Attendance> Attendances
        {
            get
            {
                return repositoryProvider.GetRepositoryForEntityType<Attendance>();
            }
        }
    }
}