using System.Data.Entity.ModelConfiguration;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data.Configuration
{
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
        }
    }
}