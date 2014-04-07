using System.Data.Entity;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbInitializer : DropCreateDatabaseIfModelChanges<AttendanceDbContext>
    {
        protected override void Seed(AttendanceDbContext context)
        {

        }
    }
}