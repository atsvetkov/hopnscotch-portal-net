using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
    }

    public interface IContactRepository : IRepository<Contact>
    {
    }

    public interface ILeadRepository : IRepository<Lead>
    {
    }
}