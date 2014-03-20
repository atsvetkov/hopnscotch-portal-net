using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public sealed class Contact : NamedBusinessEntityBase
    {
        public Contact()
        {
            Leads = new List<Lead>();
        }

        public int LevelId { get; set; }

        public ICollection<Lead> Leads { get; set; }
    }
}