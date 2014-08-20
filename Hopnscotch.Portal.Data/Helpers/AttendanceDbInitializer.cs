using System.Data.Entity;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbInitializer : CreateDatabaseIfNotExists<AttendanceDbContext>
    {
    }
}