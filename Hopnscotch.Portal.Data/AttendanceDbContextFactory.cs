namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbContextFactory : IAttendanceDbContextFactory
    {
        private readonly AttendanceDbContext _context;

        public AttendanceDbContextFactory()
        {
            _context = new AttendanceDbContext();
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ValidateOnSaveEnabled = false;
        }

        public AttendanceDbContext GetContext()
        {
            return _context;
        }
    }
}