namespace Hopnscotch.Portal.Data
{
    public interface IAttendanceDbContextFactory
    {
        AttendanceDbContext GetContext();
    }
}
