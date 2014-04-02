using System.Data.Entity.ModelConfiguration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data.Configuration
{
    public class LessonConfiguration : EntityTypeConfiguration<Lesson>
    {
        public LessonConfiguration()
        {
            HasRequired(l => l.Lead)
                .WithMany(l => l.Lessons);
        }
    }
}