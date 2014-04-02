using System.Data.Entity;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbInitializer : DropCreateDatabaseAlways<AttendanceDbContext>
    {
        protected override void Seed(AttendanceDbContext context)
        {

        }
    }
}