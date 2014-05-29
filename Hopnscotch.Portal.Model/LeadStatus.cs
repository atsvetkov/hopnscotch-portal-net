using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class LeadStatus : AmoNamedEntityBase, IUpdatableFrom<LeadStatus>
    {
        public LeadStatus()
        {
            Leads = new List<Lead>();
        }

        public string Color { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }

        public void CopyValuesFrom(LeadStatus entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Name = entity.Name;
        }
    }
}