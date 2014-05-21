using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Level : AmoNamedEntityBase, IUpdatableFrom<Level>
    {
        public Level()
        {
            Leads = new List<Lead>();
        }

        public virtual ICollection<Lead> Leads { get; set; }

        public void CopyValuesFrom(Level entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Name = entity.Name;
        }
    }
}