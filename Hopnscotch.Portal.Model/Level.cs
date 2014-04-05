using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Level : AmoEntityBase
    {
        public Level()
        {
            Leads = new List<Lead>();
        }

        public string Name { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
    }
}