using System.Data.Entity.ModelConfiguration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data.Configuration
{
    public class LeadConfiguration : EntityTypeConfiguration<Lead>
    {
        public LeadConfiguration()
        {
            HasMany(l => l.Contacts)
                .WithMany(c => c.Leads);
            
            HasMany(l => l.Lessons)
                .WithRequired(l => l.Lead);

            HasRequired(l => l.ResponsibleUser)
                .WithMany(u => u.Leads);
        }
    }
}