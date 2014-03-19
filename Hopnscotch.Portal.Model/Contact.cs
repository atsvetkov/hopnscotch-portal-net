using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Contact : NamedBusinessEntityBase
    {
        public int LevelId { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
    }
}