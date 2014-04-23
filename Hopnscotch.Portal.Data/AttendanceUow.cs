using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Data.Helpers;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class AttendanceUow : IAttendanceUow
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly AttendanceDbContext _context;

        public AttendanceUow(IRepositoryProvider repositoryProvider, IAttendanceDbContextFactory dbContextFactory)
        {
            _repositoryProvider = repositoryProvider;
            _context = dbContextFactory.GetContext();
            _repositoryProvider.DbContext = _context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<Lesson> Lessons
        {
            get
            {
                return _repositoryProvider.GetRepositoryForEntityType<Lesson>();
            }
        }

        public IRepository<Attendance> Attendances
        {
            get
            {
                return _repositoryProvider.GetRepositoryForEntityType<Attendance>();
            }
        }

        public IContactRepository Contacts
        {
            get
            {
                return _repositoryProvider.GetRepository<IContactRepository>();
            }
        }

        public ILeadRepository Leads
        {
            get
            {
                return _repositoryProvider.GetRepository<ILeadRepository>();
            }
        }

        public ITaskRepository Tasks
        {
            get
            {
                return _repositoryProvider.GetRepository<ITaskRepository>();
            }
        }

        public IUserRepository Users
        {
            get
            {
                return _repositoryProvider.GetRepository<IUserRepository>();
            }
        }

        public ILevelRepository Levels
        {
            get
            {
                return _repositoryProvider.GetRepository<ILevelRepository>();
            }
        }
    }
}