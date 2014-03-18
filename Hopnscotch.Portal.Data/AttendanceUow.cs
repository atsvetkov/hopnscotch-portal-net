using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Data.Helpers;

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

        public ILessonRepository Lessons
        {
            get
            {
                return repositoryProvider.GetRepository<ILessonRepository>();
            }
        }

        public IAttendanceRepository Attendances
        {
            get
            {
                return repositoryProvider.GetRepository<IAttendanceRepository>();
            }
        }
    }
}