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
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new LeadConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LessonConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
        }

        static AttendanceDbContext()
        {
            Database.SetInitializer(new AttendanceDbInitializer());
        }
    }
}
