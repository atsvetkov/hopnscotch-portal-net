using System;
using System.Data.Entity;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbInitializer : DropCreateDatabaseAlways<AttendanceDbContext>
    {
        protected override void Seed(AttendanceDbContext context)
        {

        }
    }
}