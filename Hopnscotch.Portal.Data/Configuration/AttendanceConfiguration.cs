using System.Data.Entity.ModelConfiguration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data.Configuration
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasRequired(a => a.Lesson)
                .WithMany(l => l.AttendanceList)
                .HasForeignKey(a => a.LessonId);
        }
    }
}