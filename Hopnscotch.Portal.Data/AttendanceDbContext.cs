using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Hopnscotch.Portal.Data.Configuration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext() : base("AttendanceDb")
        {   
        }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LessonConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
        }

        static AttendanceDbContext()
        {
            Database.SetInitializer(new AttendanceDbInitializer());
        }
    }
}
