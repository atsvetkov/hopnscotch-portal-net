using System;
using System.Data.Entity;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    internal sealed class AttendanceDbInitializer : DropCreateDatabaseIfModelChanges<AttendanceDbContext>
    {
        protected override void Seed(AttendanceDbContext context)
        {
            var lesson = new Lesson
            {
                Date = DateTime.Now,
                LeadId = 123
            };

            context.Lessons.Add(lesson);
            context.SaveChanges();
        }
    }
}