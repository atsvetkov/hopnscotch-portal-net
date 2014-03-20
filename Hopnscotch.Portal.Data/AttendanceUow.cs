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

        public IRepository<Contact> Contacts
        {
            get
            {
                return _repositoryProvider.GetRepositoryForEntityType<Contact>();
            }
        }

        public IRepository<Lead> Leads
        {
            get
            {
                return _repositoryProvider.GetRepositoryForEntityType<Lead>();
            }
        }

        public IRepository<Task> Tasks
        {
            get
            {
                return _repositoryProvider.GetRepositoryForEntityType<Task>();
            }
        }
    }
}