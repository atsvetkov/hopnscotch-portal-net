using System.Data.Entity;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class AttendanceRepository : EFRepository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(DbContext context)
            : base(context)
        {
        }
    }
}