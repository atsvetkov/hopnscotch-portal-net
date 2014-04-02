using System.Data.Entity.ModelConfiguration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data.Configuration
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasRequired(a => a.Lesson)
                .WithMany(l => l.Attendances)
                .HasForeignKey(a => a.LessonId);

            HasRequired(a => a.Contact)
                .WithMany(l => l.Attendances)
                .HasForeignKey(a => a.ContactId);
        }
    }
}