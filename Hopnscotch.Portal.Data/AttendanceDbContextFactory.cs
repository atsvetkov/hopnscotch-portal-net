using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbContextFactory : IAttendanceDbContextFactory
    {
        private readonly AttendanceDbContext _context;

        public AttendanceDbContextFactory(IConfig config)
        {
            _context = new AttendanceDbContext();
            _context.Database.Connection.ConnectionString = config.DbConnectionString;
            // _context.Configuration.ProxyCreationEnabled = false;
            //_context.Configuration.LazyLoadingEnabled = false;
            // _context.Configuration.ValidateOnSaveEnabled = false;
        }

        public AttendanceDbContext GetContext()
        {
            return _context;
        }
    }
}